var dtable;

$(document).ready(function () {

    dtable = $('#myTabledocument').DataTable({

        "ajax": {
            "url": "/DocumentDetails/AllDocument"
        },
        
        "columns": [
            { "data": "DocumentCategoryname"},
            { "data": "DocumentReferenceName" },
            {
                "data": "DocumentDate",
                "render": function (data) {
                    var MyDate_String_Value = data
                    var value = new Date
                        (
                            parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
                        );
                    var dat = value.getMonth() +
                        1 +
                        "/" +
                        value.getDate() +
                        "/" +
                        value.getFullYear();
                    return dat;
                }
            },
            { "data": "DocumentName" },
            { "data": "DocumentNameBangla" },
            { "data": "Description"},
            {
                "data": "DocumentyIdentity",
                "render": function (data) {
                    return `
                         <a onClick=EditCategory("/DocumentDetails/Edit/${data}") type="submit"  class="btn btn-info"><i class="bi bi-pencil-square"></i>  Edit</a> &nbsp;
                         <a  onClick=RemoveCategory("/DocumentDetails/Delete/${data}") type="submit" class="btn btn-danger "><i class="bi bi-trash"></i>  Delete</a>
                    `
                }
            },

        ]
    });
});

function valuex(data) {
    var MyDate_String_Value = data
    var value = new Date
        (
            parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
    );
    var yyyy = value.getFullYear().toString();
    var mm = (value.getMonth() + 1).toString();
    var dd = value.getDate().toString();
    var mmChars = mm.split('');
    var ddChars = dd.split('');
    return yyyy + '-' + (mmChars[1] ? mm : "0" + mmChars[0]) + '-' + (ddChars[1] ? dd : "0" + ddChars[0]);
}

function EditCategory(url) {    
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $("#DocumentyIdentity").val(data.document.DocumentyIdentity);
            $("#CategoryId").val(data.document.DocumentCategoryID).change();
            $("#DocumentReferenceName").val(data.document.DocumentReferenceName);
            $("#DocumentDate").val(valuex(data.document.DocumentDate));
            $("#DocumentName").val(data.document.DocumentName);
            $("#DocumentNameBangla").val(data.document.DocumentNameBangla);
            $("#Description").val(data.document.Description);
            $("#btnsubmit").text("Update");            
        }
      
    });
}

function RemoveCategory(url) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                    }
                }
            });
        }
    })
}