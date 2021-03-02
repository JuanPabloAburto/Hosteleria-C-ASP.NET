var Fn = {
    // Valida el rut con su cadena completa "XXXXXXXX-X"
    validaRut: function (rutCompleto) {
        rutCompleto = rutCompleto.replace("‐", "-");
        if (!/^[0-9]+[-|‐]{1}[0-9kK]{1}$/.test(rutCompleto))
            return false;
        var tmp = rutCompleto.split('-');
        var digv = tmp[1];
        var rut = tmp[0];
        if (digv == 'K') digv = 'k';

        return (Fn.dv(rut) == digv);
    },
    dv: function (T) {
        var M = 0, S = 1;
        for (; T; T = Math.floor(T / 10))
            S = (S + T % 10 * (9 - M++ % 6)) % 11;
        return S ? S - 1 : 'k';
    }
}

var Rut = document.getElementById('textRut')

// rutEmpresa
var rutEmpresa = document.getElementById('textrRutEmpresa')

Rut.addEventListener('input', validarRut)
rutEmpresa.addEventListener('input', validarRut)

function validarRut() {
    console.log(Rut.value)

    if (Fn.validaRut(Rut.value)) {

        Rut.className += "form-control is-valid"
    } else {
        Rut.className += "form-control is-invalid"

    }

    if (Fn.validaRut(rutEmpresa.value)) {

        rutEmpresa.className += "form-control is-valid"
    } else {
        rutEmpresa.className += "form-control is-invalid"

    }
}


// nombre
var Nombre = document.getElementById('textNombre')
// apellidoPaterno
var apellidoPaterno = document.getElementById('textApellidoPaterno')
// apellidoMaterno
var apellidoMaterno = document.getElementById('textApellidoMaterno')





// telefono
var telefono = document.getElementById('textTelefono')
// idSexo
var idSexo = document.getElementById('textIdSexo')
// imail
var imaiL = document.getElementById('textImail')

// botones de enviar

var BtnEnviar = document.getElementById('BtnEnviar');


Nombre.addEventListener('input', validarLargo)
Nombre.addEventListener('input', validarLargo)
apellidoPaterno.addEventListener('input', validarLargo)
apellidoMaterno.addEventListener('input', validarLargo)
rutEmpresa.addEventListener('input', validarLargo)
telefono.addEventListener('input', validarLargo)
idSexo.addEventListener('input', validarLargo)
imaiL.addEventListener('input', validarLargo)

/// Huesped validacion


function validarLargo(e) {
    //nombre
    if (Nombre.value.length > 0) {
        Nombre.className += "form-control is-valid"
    } else {
        Nombre.className += "form-control is-invalid"
    }
    //apellidoPaterno
    if (apellidoPaterno.value.length > 0) {
        apellidoPaterno.className += "form-control is-valid"
    } else {
        apellidoPaterno.className += "form-control is-invalid"
    }
    //apellidoMaterno
    if (apellidoMaterno.value.length > 0) {
        apellidoMaterno.className += "form-control is-valid"
    } else {
        apellidoMaterno.className += "form-control is-invalid"
    }  //rutEmpresa
   /* if (rutEmpresa.value.length > 0) {
        rutEmpresa.className += "form-control is-valid"
    } else {
        rutEmpresa.className += "form-control is-invalid"
    }   */
    //telefono
    if (telefono.value.length > 0) {
        telefono.className += "form-control is-valid"
    } else {
        telefono.className += "form-control is-invalid"
    }
    //idSexo
    if (idSexo.value.length > 0) {
        idSexo.className += "form-control is-valid"
    } else {
        idSexo.className += "form-control is-invalid"
    }
    //imaiL
    if (imaiL.value.length > 0) {
        imaiL.className += "form-control is-valid"
    } else {
        imaiL.className += "form-control is-invalid"
    }

    if (
        Nombre.value.length > 0 &&
        apellidoPaterno.value.length > 0 &&
        apellidoMaterno.value.length > 0 &&
        rutEmpresa.value.length > 0 &&
        telefono.value.length > 0 &&
        idSexo.value.length > 0 &&
        imaiL.value.length > 0
    ) {
        BtnEnviar.className += "btn btn-primary"
    } else {
        BtnEnviar.className += "btn btn-primary disabled"
    }


}



