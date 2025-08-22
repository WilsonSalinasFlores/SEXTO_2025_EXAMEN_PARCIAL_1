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
    optionSelecionar.textContent = 'Seleccione un Evento';
    optionSelecionar.value = '-1';
    optionSelecionar.disabled = true;
    optionSelecionar.selected = true;
    select.appendChild(optionSelecionar);

    eventos.forEach(evento => {
        const option = document.createElement('option');
        option.value = evento.eventoId;
        option.textContent = evento.nombre;
        select.appendChild(option);
    });
    
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

    const responseParticipantes = await fetch(`/api/ParticipantesApi/${eventoId}`);
    const participantes = await responseParticipantes.json();

    const tbody = document.getElementById('listaParticipantes');
    tbody.innerHTML = '';

    participantes.forEach(participante => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${participante.nombre}</td>
            <td>${participante.email}</td>
            <td>${participante.telefono}</td>
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

    if (!eventoId || !participanteId) {
        alert('Por favor, seleccione un participante.');
        return;
    }

    const response = await fetch('/api/InscripcionesApi', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            eventoId: eventoId,
            participanteId: participanteId
        })
    });

    if (response.ok) {
        alert('Inscripción guardada exitosamente.');
        location.reload();
    } else {
        alert('Error al guardar la inscripción.');
    }
};
