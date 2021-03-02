// V A L I D A C I Ó N

// ---------------------------------------------------
// ----------- O R D E N  D E  C O M P R A -----------
// ---------------------------------------------------
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

// ----------- RutHuesped -----------
var RutHuesped = document.getElementById('textRutHuesped')
// ----------- RutEmpleado -----------
var RutEmpresa = document.getElementById('textRutEmpresa')

// ----------- Rut empleado --------
var RutEmpleado = document.getElementById('textRutEmpleado')

RutHuesped.addEventListener('input', validarRut)
RutEmpresa.addEventListener('input', validarRut)
RutEmpleado.addEventListener('input', validarRut)
function validarRut() {
    console.log(RutEmp.value)

    if (Fn.validaRut(RutHuesped.value)) {
        RutHuesped.className += "form-control is-valid"
    } else {
        RutHuesped.className += "form-control is-invalid"
    }

    if (Fn.validaRut(RutEmpresa.value)) {
        RutEmpresa.className += "form-control is-valid"
    } else {
        RutEmpresa.className += "form-control is-invalid"
    }

    if (Fn.validaRut(RutEmpleado.value)) {
        RutEmpresa.className += "form-control is-valid"
    } else {
        RutEmpresa.className += "form-control is-invalid"
    }

}



// ----------- NumeroOc -----------
/*var NumeroOc = document.getElementById('textNumeroOc')*/
// ----------- FechaIngreso -----------
var FechaIngreso = document.getElementById('textFechaIngreso')
// ----------- FechaSalida -----------
var FechaSalida = document.getElementById('textFechaSalida')

// ----------- EmailEmpresa -----------
var EmailEmpresa = document.getElementById('textEmailEmpresa')
// ----------- IdComedor -----------
var IdComedor = document.getElementById('textIdComedor')
// ----------- NHabitacion -----------
var NHabitacion = document.getElementById('textNHabitacion')
// ----------- IdEstado -----------
var IdEstado = document.getElementById('textIdEstado')
// ----------- EmailEmpresa -----------
var EmailEmpleado = document.getElementById('textEmailEmpleado')






// ----------- botones de enviar -----------
var BtnEnviar = document.getElementById('BtnEnviar');




// ----------- addEventListener -----------
NumeroOc.addEventListener('input', validarLargo)
FechaIngreso.addEventListener('input', validarLargo)
FechaSalida.addEventListener('input', validarLargo)
RutHuesped.addEventListener('input', validarLargo)
RutEmpresa.addEventListener('input', validarLargo)
EmailEmpresa.addEventListener('input', validarLargo)
IdComedor.addEventListener('input', validarLargo)
NumeroOc.addEventListener('input', validarLargo)
IdEstado.addEventListener('input', validarLargo)
RutEmpleado.addEventListener('input', validarLargo)
EmailEmpleado.addEventListener('input', validarLargo)


// F U N C I O N   V A L I D A R  L A R G O 

function validarLargo(e) {


  //FechaIngreso
  if (FechaIngreso.value.length > 0) {
    FechaIngreso.className += "form-control is-valid"
  } else {
    FechaIngreso.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //FechaSalida
  if (FechaSalida.value.length > 0) {
    FechaSalida.className += "form-control is-valid"
  } else {
    FechaSalida.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
 
  //EmailEmpresa
  if (EmailEmpresa.value.length > 0) {
    EmailEmpresa.className += "form-control is-valid"
  } else {
    EmailEmpresa.className += "form-control is-valid"
      .className += "form-control is-invalid"
    }
    //EmailEmpleado
    if (EmailEmpleado.value.length > 0) {
        EmailEmpleado.className += "form-control is-valid"
    } else {
        EmailEmpleado.className += "form-control is-valid"
            .className += "form-control is-invalid"
    }

  //IdComedor
  if (IdComedor.value.length > 0) {
    IdComedor.className += "form-control is-valid"
  } else {
    IdComedor.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }

  //NHabitacion
  if (NHabitacion.value.length > 0) {
    NHabitacion.className += "form-control is-valid"
  } else {
    NHabitacion.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }
  //IdEstado
  if (IdEstado.value.length > 0) {
    IdEstado.className += "form-control is-valid"
  } else {
    IdEstado.className += "form-control is-valid"
      .className += "form-control is-invalid"
  }





  // ----- V A L I D A R  B O T O N -----
  if (
    FechaIngreso.value.length > 0 &&
       FechaIngreso.value.length > 0 &&
    FechaSalida.value.length > 0 &&
    RutHuesped.value.length.length > 0 &&
    RutEmpresa.value.length > 0 &&
    EmailEmpresa.value.length > 0 &&
    IdComedor.value.length > 0 &&
    NHabitacion.value.length > 0 &&
      IdEstado.value.length > 0 &&
      RutEmpleado.value.length > 0 &&
      EmailEmpleado.value.length > 0 
  ) {
    BtnEnviar.className += "btn btn-primary"
  } else {
    BtnEnviar.className += "btn btn-primary disabled"
  }


}


