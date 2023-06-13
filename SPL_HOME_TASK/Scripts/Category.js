var dtable;

$(document).ready(function () {

    dtable = $('#myTable').DataTable({

        "ajax": {
            "url": "/CategoryInformation/AllCategory"
        },

        "columns": [
            { "data": "CategoryName"},
            { "data": "CategoryNameBangla"},
            { "data": "Description"},
            {
                "data": "CategoryId",
                "render": function (data) {
                    return `
                         <a onClick=EditCategory("/CategoryInformation/Edit/${data}") type="submit"  class="btn btn-info"><i class="bi bi-pencil-square"></i>  Edit</a> &nbsp;
                         <a  onClick=RemoveCategory("/CategoryInformation/Delete/${data}") type="submit" class="btn btn-danger "><i class="bi bi-trash"></i>  Delete</a>
                    `
                }
            },

        ]
    });
});



function EditCategory(url) {    
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $("#CategoryId").val(data.catgory.CategoryId);
            $("#CategoryName").val(data.catgory.CategoryName);
            $("#CategoryNameBangla").val(data.catgory.CategoryNameBangla);
            $("#Description").val(data.catgory.Description);
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