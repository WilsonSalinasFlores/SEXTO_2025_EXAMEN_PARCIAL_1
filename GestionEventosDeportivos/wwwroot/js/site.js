// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var cargarListaEventos = async (todos) => {

    var selectClear = document.getElementById('listaEventos');
    while (selectClear.firstChild) {
        selectClear.removeChild(selectClear.firstChild);
    }
    document.getElementById('nombreEvento').value ="";
    document.getElementById('fechaEvento').value = "";
    document.getElementById('ubicacionEvento').value = "";
    document.getElementById('descripcionEvento').value = "";
    var eventos = [];
    if (todos) {
        const response = await fetch('/api/EventosApi');
        eventos = await response.json();
    }else{
        const responseActivos = await fetch('/api/EventosApi/activos');
        eventos = await responseActivos.json();
    }
    const select = document.getElementById('listaEventos');
    const optionSelecionar = document.createElement('option');
    
    
    select.appendChild(optionSelecionar);

    eventos.forEach(evento => {
        const option = document.createElement('option');
        option.value = evento.eventoId;
        option.textContent = evento.nombre;
        select.appendChild(option);
    });

    
    optionSelecionar.textContent = 'Seleccione un Evento';
    optionSelecionar.value = '-1';
    optionSelecionar.disabled = true;
    optionSelecionar.selected = true;
    select.value = '-1';
    const eventoGuardado = localStorage.getItem("evento");
    if (eventoGuardado) {
        const estado = JSON.parse(eventoGuardado);
        select.value = estado.filtro;
        var sw=0
        eventos.forEach(evento => {
            if (evento.eventoId == estado.filtro) {
                cargarListaParticipantes(estado.filtro);
                sw=1;
            }
        });
        if (sw==0){
            select.value = '-1';
        }
    }

    
};
var cargarListaParticipantes = async (eventoId) => {
    //obtener el detalle del evento seleccionado
    const response = await fetch(`/api/EventosApi/${eventoId}`);
    const evento = await response.json();

    if (evento) {
        document.getElementById('nombreEvento').value = evento.nombre;
        document.getElementById('fechaEvento').value = evento.fecha;
        document.getElementById('ubicacionEvento').value = evento.ubicacion;
        document.getElementById('descripcionEvento').value = evento.descripcion;
    }

    const estado = {
        filtro: document.getElementById("listaEventos").value,
        pagina: window.location.pathname
    };

    localStorage.setItem("evento", JSON.stringify(estado));
    const responseParticipantes = await fetch(`/api/ParticipantesApi/ParticipantesPorEvento/${eventoId}`);
    const participantes = await responseParticipantes.json();

    const tbody = document.getElementById('listaTablaParticipantes');
    tbody.innerHTML = '';

    participantes.forEach(participante => {
        const tr = document.createElement('tr');
        tr.setAttribute('id', participante.inscripcionId);
        tr.setAttribute('id-participante', participante.participanteId);
        
        const fecha = participante.fechaInscripcion ? participante.fechaInscripcion.split('T')[0] : '';
        tr.innerHTML = `
            <td>${participante.nombre.trim()} ${participante.apellido.trim()}</td>
            <td>${participante.email.trim()}</td>
            <td>${participante.telefono.trim()}</td>
            <td>${fecha}</td>
            <td>
            <button class="btn btn-danger" onclick="eliminarInscripcion(${participante.inscripcionId})">Eliminar</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
};

var nuevoParticipante = async () => {
    if (document.getElementById('listaEventos').value=='-1') {
        alert('Por favor, seleccione un evento.');
        
        var modal = bootstrap.Modal.getInstance(document.getElementById('nuevoParticipanteModal'));
        modal.hide();
        location.reload();
        return;
    }
    // Cargar participantes en el combo
    const response = await fetch('/api/ParticipantesApi');
    const participantes = await response.json();

    const select = document.getElementById('listaParticipantesModal');
    while (select.firstChild) {
        select.removeChild(select.firstChild);
    }

    const optionSeleccionar = document.createElement('option');
    optionSeleccionar.textContent = 'Seleccione un Participante';
    optionSeleccionar.disabled = true;
    optionSeleccionar.selected = true;
    select.appendChild(optionSeleccionar);

    participantes.forEach(participante => {
        const option = document.createElement('option');
        option.value = participante.participanteId;
        option.textContent = participante.nombre.trim() + ' ' + participante.apellido.trim();
        select.appendChild(option);
    });
};

var cargarParticipantesModal = async (participanteId) => {
    const response = await fetch(`/api/ParticipantesApi/${participanteId}`);
    const participante = await response.json();

    if (participante) {
        document.getElementById('nuevoParticipanteEmail').value = participante.email;
        document.getElementById('nuevoParticipanteTelefono').value = participante.telefono;
    }
};

var guardarInscripcion = async () => {
    const eventoId = document.getElementById('listaEventos').value;
    const participanteId = document.getElementById('listaParticipantesModal').value;

    if (!eventoId || !participanteId || eventoId === '-1' || participanteId === '-1') {
        alert('Por favor, seleccione un participante.');
        return;
    }
    //Validar que no se haya insertado un participante
    const responseValidar = await fetch(`/api/InscripcionesApi/ValidarParticipante/${eventoId}/${participanteId}`);
    const existeInscripcion = await responseValidar.json();

    if (existeInscripcion) {
        alert('El participante ya está inscrito en este evento.');
        return;
    }

    var inscripcion={
        inscripcionId: 0,
        eventoId: eventoId,
        participanteId: participanteId,
        fechaInscripcion: new Date().toISOString()
    }

    try {
        
        const response = await fetch('/api/InscripcionesApi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(inscripcion)
        });

        if (response.ok) {
            alert('Inscripción guardada exitosamente.');
            var modal = bootstrap.Modal.getInstance(document.getElementById('nuevoParticipanteModal'));
            modal.hide();
            location.reload();
        } else {
           alert(`Error al guardar la inscripción. ${response.statusText}`);
        }
    } catch (error ) {
        console.error('Error al guardar la inscripción:', error);
    }
};

var eliminarInscripcion = async (inscripcionId) => {
    if (confirm('¿Estás seguro de que deseas eliminar esta inscripción?')) {
        try {
            const response = await fetch(`/api/InscripcionesApi/${inscripcionId}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                
                const eventoId = document.getElementById('listaEventos').value;
                location.reload();
                document.getElementById('listaEventos').value = eventoId;
            } else {
                alert(`Error al eliminar la inscripción. ${response.statusText}`);
            }
        } catch (error) {
            console.error('Error al eliminar la inscripción:', error);
        }
    }
};
