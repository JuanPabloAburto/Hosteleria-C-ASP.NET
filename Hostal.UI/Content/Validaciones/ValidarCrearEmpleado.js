// V A L I D A C I Ó N

// ---------------------------------------
// ----------- E M P L E A D O -----------
// ---------------------------------------


// F U N C I O N  V A L I D A R  R U T 

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

// ----------- RutEmp -----------
var RutEmp = document.getElementById('textRutEmp')



RutEmp.addEventListener('input', validarRut)

function validarRut() {
    console.log(RutEmp.value)

    if (Fn.validaRut(RutEmp.value)) {
        RutEmp.className += "form-control is-valid"
    } else {
        RutEmp.className += "form-control is-invalid"
    }
}

// ----------- NombreEmp -----------
var NombreEmp = document.getElementById('textNombreEmp')
// ----------- ApellidoPaterno -----------
var ApellidoPaterno = document.getElementById('textApellidoPaterno')
// ----------- ApellidoMaterno -----------
var ApellidoMaterno = document.getElementById('textApellidoMaterno')
// ----------- FechaNacimiento -----------
var FechaNacimiento = document.getElementById('textFechaNacimiento')
// ----------- FechaIngreso -----------
var FechaIngreso = document.getElementById('textFechaIngreso')
// ----------- UsuarioEmail -----------
var UsuarioEmail = document.getElementById('textUsuarioEmail')
// ----------- IdSexo -----------
var IdSexo = document.getElementById('texIdSexo')
//------------Telefono
var Telefono = document.getElementById('textTelefono')

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
RutEmp.addEventListener('input', validarLargo)
NombreEmp.addEventListener('input', validarLargo)
ApellidoPaterno.addEventListener('input', validarLargo)
ApellidoMaterno.addEventListener('input', validarLargo)
FechaNacimiento.addEventListener('input', validarLargo)
FechaIngreso.addEventListener('input', validarLargo)
Telefono.addEventListener('input', validarLargo)
UsuarioEmail.addEventListener('input', validarLargo)
IdRegion.addEventListener('input', validarLargo)


// -------------------------------------
// ----------- U S U A R I O -----------
// -------------------------------------
ContrasenaUser.addEventListener('input', validarLargo)
EstadoUsuario.addEventListener('input', validarLargo)
TipoUsuario.addEventListener('input', validarLargo)










// F U N C I O N   V A L I D A R  L A R G O 

function validarLargo(e) {
    /*
  //RutEmp
  if (RutEmp.value.length > 0) {
    RutEmp.className += "form-control is-valid"
  } else {
    RutEmp.className += "form-control is-invalid"
  } */
  //NombreProv
  if (NombreEmp.value.length > 0) {
    NombreEmp.className += "form-control is-valid"
  } else {
    NombreEmp.className += "form-control is-invalid"
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
    ApellidoMaterno.className += "form-control is-invalid"
  }
  //TelefonoProv
  if (Telefono.value.length > 0) {
    Telefono.className += "form-control is-valid"
  } else {
    Telefono.className += "form-control is-invalid"
  }
  //FechaNacimiento
  if (FechaNacimiento.value.length > 0) {
    FechaNacimiento.className += "form-control is-valid"
  } else {
    FechaNacimiento.className += "form-control is-invalid"
    }
    //FechaIngreso
    if (FechaIngreso.value.length > 0) {
        FechaIngreso.className += "form-control is-valid"
    } else {
        FechaIngreso.className += "form-control is-invalid"
    }
  //UsuarioEmail
  if (UsuarioEmail.value.length > 0) {
    UsuarioEmail.className += "form-control is-valid"
  } else {
    UsuarioEmail.className += "form-control is-invalid"
  }
  //IdSexo
  if (IdSexo.value.length > 0) {
    IdSexo.className += "form-control is-valid"
  } else {
    IdSexo.className += "form-control is-invalid"
  }

  // -------------------------------------
  // ----------- U S U A R I O -----------
  // -------------------------------------


  //TipoUsuario
  if (TipoUsuario.value.length > 0) {
    TipoUsuario.className += "form-control is-valid"
  } else {
    TipoUsuario.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //EstadoUsuario
  if (EstadoUsuario.value.length > 0) {
    EstadoUsuario.className += "form-control is-valid"
  } else {
    EstadoUsuario.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //PasswordUsuario
  if (ContrasenaUser.value.length > 0) {
    ContrasenaUser.className += "form-control is-valid"
  } else {
    ContrasenaUser.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }


  // ----- V A L I D A R  B O T O N -----
  if (
    RutEmp.value.length > 0 &&
    NombreEmp.value.length>00 &&
    ApellidoPaterno.value.length>0 &&
    ApellidoMaterno.value.length>0 &&
    Telefono.value.length>0 &&
      FechaNacimiento.value.length > 0 &&
      FechaIngreso.value.length > 0 &&
    UsuarioEmail.value.length>0 &&
    IdSexo.value.length>0 &&
    EstadoUsuario.value.length>0 &&
    TipoUsuario.value.length>0 &&
    ContrasenaUser.value.length>0

  ) {
    BtnEnviar.className += "btn btn-primary"
  } else {
    BtnEnviar.className += "btn btn-primary disabled"
  }

}


