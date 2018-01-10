//obtiene el value de las tablas html y lo concatena en un objeto
function inicializar() {
    var x=$("#guardar");
        x.click(obtener);
    }

function obtener() {
    var col1_value = "";

    $("#edit-table tr").each(function () {
        var currentRow = $(this);
        col1_value += (currentRow.find("td:eq(0)").text());
    });
    console.log(col1_value);

    $("#edit-table tr").each(function () {
        var currentRow = $(this);
        col1_value += (currentRow.find("td:eq(0)").text());
    });
    console.log(col1_value);
  }
