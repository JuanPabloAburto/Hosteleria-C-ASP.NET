
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


// ----------- RutProv -----------
var RutProv = document.getElementById('textRutProv')

RutProv.addEventListener('input', validarRut)

function validarRut() {
    console.log(RutProv.value)

    if (Fn.validaRut(RutProv.value)) {
        RutProv.className += "form-control is-valid"
    } else {
        RutProv.className += "form-control is-invalid"
    }
}




// ----------- NombreProv -----------
var NombreProv = document.getElementById('textNombreProv')
// ----------- ApellidoPaterno -----------
var ApellidoPaterno = document.getElementById('textApellidoPaterno')
// ----------- ApellidoMaterno -----------
var ApellidoMaterno = document.getElementById('textApellidoMaterno')
// ----------- TelefonoProv -----------
var TelefonoProv = document.getElementById('textTelefonoProv')
// ----------- UsuarioEmail -----------
var UsuarioEmail = document.getElementById('textUsuarioEmail')
// ----------- IdRegion -----------
var IdRegion = document.getElementById('textIdRegion')

// -------------------------------------
// ----------- U S U A R I O -----------
// -------------------------------------


// ----------- ContrasenaUser -----------
var ContrasenaUser = document.getElementById('textContrasena')
// ----------- EstadoUsuario -----------
var EstadoUsuario = document.getElementById('textEstadoUsuario')
// ----------- TipoUsuario -----------
var TipoUsuario = document.getElementById('textTipoUsuario')


// ----------- botones de enviar -----------
var BtnEnviar = document.getElementById('BtnEnviar');

// ----------- addEventListener -----------

NombreProv.addEventListener('input', validarLargo)
ApellidoPaterno.addEventListener('input', validarLargo)
ApellidoMaterno.addEventListener('input', validarLargo)
TelefonoProv.addEventListener('input', validarLargo)
UsuarioEmail.addEventListener('input', validarLargo)
IdRegion.addEventListener('input', validarLargo)
// -------------------------------------
// ----------- U S U A R I O -----------
// -------------------------------------
ContrasenaUser.addEventListener('input', validarLargo)
EstadoUsuario.addEventListener('input', validarLargo)
TipoUsuario.addEventListener('input', validarLargo)


// F U N C I O N  V A L I D A R  R U T 











// F U N C I O N   V A L I D A R  L A R G O 

function validarLargo(e) {
  //RutProv
 /* if (RutProv.value.length > 0) {
    RutProv.className += "form-control is-valid"
  } else {
    RutProv.className += "form-control is-invalid"
  } */
  //NombreProv
  if (NombreProv.value.length > 0) {
    NombreProv.className += "form-control is-valid"
  } else {
    NombreProv.className += "form-control is-invalid"
  }
  //ApellidoPaterno
  if (ApellidoPaterno.value.length > 0) {
    ApellidoPaterno.className += "form-control is-valid"
  } else {
    ApellidoPaterno.className += "form-control is-invalid"
  }
  //ApellidoMaterno
  if (ApellidoMaterno.value.length > 0) {
    ApellidoMaterno.className += "form-control is-valid"
  } else {
    ApellidoMaterno.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //TelefonoProv
  if (TelefonoProv.value.length > 0) {
    TelefonoProv.className += "form-control is-valid"
  } else {
    TelefonoProv.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //UsuarioEmail
  if (UsuarioEmail.value.length > 0) {
    UsuarioEmail.className += "form-control is-valid"
  } else {
    UsuarioEmail.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //IdRegion
  if (IdRegion.value.length > 0) {
    IdRegion.className += "form-control is-valid"
  } else {
    IdRegion.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }

  // -------------------------------------
  // ----------- U S U A R I O -----------
  // -------------------------------------

  
  //EstadoUsurio
  if (EstadoUsuario.value.length > 0) {
    EstadoUsuario.className += "form-control is-valid"
  } else {
    EstadoUsuario.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //TipoUsuario
  if (TipoUsuario.value.length > 0) {
    TipoUsuario.className += "form-control is-valid"
  } else {
    TipoUsuario.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  // ----- V A L I D A R  B O T O N -----
  if (
    RutProv.value.length > 0 &&
    NombreProv.value.length > 0 &&
    ApellidoPaterno.value.length > 0 &&
    ApellidoMaterno.value.length > 0 &&
    TelefonoProv.value.length > 0 &&
    UsuarioEmail.value.length > 0 &&
    IdRegion.value.length > 0 &&
    ContrasenaUser.value.length > 0 &&
    EstadoUsuario.value.length > 0 &&
    TipoUsuario.value.length > 0 
    
  ) {
    BtnEnviar.className += "btn btn-primary"
  } else {
    BtnEnviar.className += "btn btn-primary disabled"
  }


}


