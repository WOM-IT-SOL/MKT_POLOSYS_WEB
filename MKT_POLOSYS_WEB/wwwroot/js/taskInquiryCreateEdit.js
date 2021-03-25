var myTable = $('#myTableDetailTask').DataTable({
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "info": true,
            "autoWidth": true,
            "data": [],
            "columns": [
                {
                    "title": "Status Prospek",
                    "data": "sourceProspek"
                },{
                    "title": "Field Person Name",
                    "data": "fieldPersonName"
                },{
                    "title": "Started Date",
                    "data": "startedDate"
                }, {
                    "title": "Submitted Date",
                    "data": "submittedDate"
                }, {
                    "title": "Priority Level",
                    "data": "priorityLevel"
                }
            ]
        });


            $.ajax({
                url: '@Url.Action("ListDetailTask")',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                async: false,
                type: 'POST',
                data: {
                    idTask:$("#taskID").val()
                },
                success: function (result) {
                    var jsonString = JSON.stringify(result);
                        myTable.clear();
                        $.each(result, function (index, value) {
                            myTable.row.add(value);
                        });
                        myTable.draw();
                }
            });