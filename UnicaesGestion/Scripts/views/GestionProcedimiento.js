

$(document).ready(init);

function init() {  
    $("#date").datepicker();
    init_stepy();
}

function init_stepy() {
        $('#wfrmpuesto').stepy({
            backLabel: 'Atras',
            block: true,
            nextLabel: 'Siguiente',
            titleClick: true,
            titleTarget: "#stepy-tab",
            legend: false               
        });   
}


function loading(obj) {
    $(obj).html("<div style='text-align:right;'><br/><img src='/Content/img/loading.gif' alt='cargando..' width='45' height='45' /> procesando..</div>");
}

function showError(obj, msj) {
    var html = "<div class='alert alert- block alert-danger fade in'>";
    html += "<button data-dismiss='alert' class='close close-sm' type= 'button' >";
    html += "<i class='fa fa-times'></i></button> <strong>Error</strong>";
    html += msj;
    html += "</div >";
    $(obj).html(html);
}

//paso 1
function ProcesarPaso1() {
    $("#wfrmpuesto").validate({
        rules: {
            nombre: {
                required:true
            },
            objetivoInicial: {
                required:true
            },            
            objetivoFinal: {
                required: true                               
            }
        },
        messages: {
            nombre: {
                required: "Indique el nombre del procedimiento"
            }, 
            objetivoInicial: {
                required: "Debe indicar el objetivo"
            },
            objetivoFinal: {
                required: "Debe indicar el objetivo"
            }
         
        },
        submitHandler: function (form) {
            //cuando el formulario es válido.
            loading($("#loading1"));
            $("#btnsiguiente1").hide();
            GuardarPaso1();

        },
        invalidHandler: function (event, validator) {
            //cuando el formulario tiene errores. 
        },
        highlight: function (element, errorClass) {
            $(element).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (element, errorClass) {
            $(element).closest(".form-group").removeClass("has-error");
        }
    });


    var d = $("#wfrmpuesto").submit();

}

function GuardarPaso1() {
    var id = $("#procedimientoid").val();
    var nombre = $("#nombre").val();
    var objetivoInicial = $("#objetivoInicial").val();
    var objetivoFinal= $("#objetivoFinal").val();  
    var action;
    var data;
    
    if (typeof id == "undefined" ||  id==="0") {
        action = "/Procedimientoes/CrearProcedimiento/";
        data = { "nombre": nombre, "objetivoInicial": objetivoInicial, "objetivoFinal": objetivoFinal }
    } else {
        action = "/Procedimientoes/ModificarProcedimiento/";
        data = { "id": id, "nombre": nombre, "objetivoInicial": objetivoInicial, "objetivoFinal": objetivoFinal }
    }

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {
           
            $("#procedimientoid").val(response.data);
            $("#btnsiguiente1").show();
            $("#loading1").html("");
            Next1();
        }, error: function (response) {
            $("#btnsiguiente1").show();
            showError($("#loading1"), response.data);
        }
    });

}

function Next1() {
    $("#default-title-1").addClass("current-step");
    $("#default-title-0").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 1);
    obteniendoFunciones();
}

//paso 2
function obteniendoFunciones() {
    
    loading($("#pasos_procedimiento"), "Cargando Procedimientos");
    var id = $("#procedimientoid").val();
    $.ajax({
        url: "/Procedimientoes/PasosProcedimiento",
        method: "POST",
        data: { id: id }, 
        success: function(response) {
            $("#pasos_procedimiento").html(response);
        },
        error: function(response) {

            showError($("#pasos_procedimiento"), "No se pudieron cargar los paso");
        }
    });

}

function dlgaddfuncion() {
    $("#txtpaso").val("");
    $("#mdaddfuncion").modal();
    $("#idpaso").val("0");
}


function GuardarFuncion() {
    $("#frmpasos").validate({
        rules: {
            txtpaso: {
                required: true
            }
        },
        messages: {
            txtfuncion: "Debe indicar un paso.."
        },
        submitHandler: function (form) {
            ajaxGuardarFuncion();

        }, highlight: function (element, errorClass) {
            $(element).closest(".form-group").addClass("has-error");
        },
        unhighlight: function (element, errorClass) {
            $(element).closest(".form-group").removeClass("has-error");
        }
    });
    $("#frmpasos").submit();

  
}

function ajaxGuardarFuncion() {
    var idpaso = $("#idpaso").val();
    var idprocedimiento = $("#procedimientoid").val();
    var numero = $("#numero").val();
    var descripcion = $("#txtpaso").val();
    var predecesores = $("#predecesores").val();
    var idTipoPaso = $("#cmbtipoPasos").val();
    var idPuestoTrabajo = $("#cmbPuestos").val();
    var action = "";
    var data;

    if (idpaso === "0") {
        action = "/Procedimientoes/AgregarPaso";
        data = { "idProcedimiento": idprocedimiento, "numero":numero ,"descripcion": descripcion,"predecesores": predecesores, "idTipoPaso":idTipoPaso, "idPuestoTrabajo": idPuestoTrabajo };

    } else {
        action = "/Procedimientoes/EditarPaso";
        data = { "idPaso":idpaso, "numero": numero, "descripcion": descripcion, "predecesores": predecesores, "idTipoPaso": idTipoPaso, "idPuestoTrabajo": idPuestoTrabajo };

    }

    $("#mdaddfuncion").modal("hide");
    loading($("#loading2"), "Procesando");

    $.ajax({
        url: action,
        method: "POST",
        data: data,
        success: function (response) {
            if (response.result === "success") {
                $("#loading2").html("");
                obteniendoFunciones();
                
            } else {
                showError($("#loading2"), response.data);
            }
            $("#idpaso").val("0");
        },
        error: function (response) {
            showError($("#loading2"), response.data);
            $("#idpaso").val("0");
        }
    });

}

function EditarFuncion(id) {
    $("#idpaso").val(id);
    var paso = $("#paso_" + id).html();
    $("#txtpaso").val(paso.trim());
    $("#mdaddfuncion").modal();
}

function Back2() {
    $("#default-title-0").addClass("current-step");
    $("#default-title-1").removeClass("current-step");
    $('#wfrmpasos').stepy("step", 0);
}

function dlgeliminar(id) {
    $("#idfunciondel").val(id);
    $("#mddelfuncion").modal();
}

function confirmarEliminar() {
    $("#mddelfuncion").modal("hide");
    loading($("loading2"), "Eliminando paso..");
    var idfuncion = $("#idfunciondel").val();
    $.ajax({
        url: "/Procedimientoes/EliminarPaso",
        method: "POST", 
        data: { "id": idfuncion },
        success: function (response) {
            if (response.result === "success") {
                obteniendoFunciones();
                $("loading2").html("");
            } else {
                showError($("loading2"), response.data);
            }

        }, error: function(response) {
            showError($("loading2"), "Error procesando transacción...");
        }
    });

}

function Next2() {
    $("#default-title-2").addClass("current-step");
    $("#default-title-1").removeClass("current-step");
    $('#wfrmpuesto').stepy("step", 2);
}




