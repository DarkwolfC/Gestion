
//$(document).ready(init);


var DataSourceTree = function (options) {
    this._data = options.data;
    this._delay = options.delay;
};

DataSourceTree.prototype = {

    data: function (options, callback) {
        var self = this;

        setTimeout(function () {
            var data = $.extend(true, [], self._data);

            callback({ data: data });

        }, this._delay)
    }
};



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

function dynamicDataSource(openedParentData, callback) {      
    var parentID = 0;
    var div = $("#loadingTree");
    loading(div, "Cargando unidades académicas");
    $.ajax({
        url: "/Unidades/GetUnidades",
        method: "POST",
        data: { "parentid": parentID },
        success: function (json) {
            $(div).html("");
            var data = [];
            var unidadesDatasource;
            console.log(json);
            for (var j in json) {
                data.push({ name: json[j].nombre + '<div class="tree-actions"><i class="fa fa-plus"></i><i class="fa fa-trash-o"></i><i class="fa fa-pencil"></i></div>', type: 'folder', additionalParameters: { id: json[j].id } });
               
            }
            
            //unidadesDatasource = new DataSourceTree({
            //    data: data,
            //    delay: 200
            //});

            childNodesArray = [
                { "name": "Ascending and Descending", "type": "folder" },
                { "name": "Sky and Water I", "type": "item" },
                { "name": "Drawing Hands", "type": "folder" },
                { "name": "waterfall", "type": "item" },
                { "name": "Belvedere", "type": "folder" },
                { "name": "Relativity", "type": "item" },
                { "name": "House of Stairs", "type": "folder" },
                { "name": "Convex and Concave", "type": "item" }
            ]
            //var childObjectsArray = unidadesDatasource;
            console.log(data);
            callback({
                "data": childNodesArray
            });
            
        },
        error: function (response) {
            showError(div, "No se pudieron cargar las unidades academicas");
        }
    });


}

function staticDataSource(openedParentData, callback) {
    childNodesArray = [
        { "name": "Ascending and Descending", "type": "folder" },
        { "name": "Sky and Water I", "type": "item" },
        { "name": "Drawing Hands", "type": "folder" },
        { "name": "waterfall", "type": "item" },
        { "name": "Belvedere", "type": "folder" },
        { "name": "Relativity", "type": "item" },
        { "name": "House of Stairs", "type": "folder" },
        { "name": "Convex and Concave", "type": "item" }
    ];

    callback({
        data: childNodesArray
    });
}

function init() {

    //$('#treeUnidades').tree({
    //    selectable: false,
    //    dataSource: staticDataSource,
    //    loadingHTML: '<img src="/Content/img/input-spinner.gif" />',
    //});

  
    $('#treeUnidades').tree({
        dataSource: dynamicDataSource,
        multiSelect: false,
        folderSelect: true
    });
    
}