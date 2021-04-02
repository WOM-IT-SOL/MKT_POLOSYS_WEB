$('#ddlPriorityLevel').multiselect({
    buttonWidth: '180px'
});

$(".caret").css('float', 'right');
$(".caret").css('margin', '8px 0');

var myTable = $('#myTableList').DataTable({
    "paging": true,
    "lengthChange": true,
    "searching": true,
    "ordering": true,
    "info": true,
    "autoWidth": true,
    "data": [],
    "columns": [
        {
            "title": "SOURCE DATA",
            "data": "sourceData"
        }, {
            "title": "CABANG",
            "data": "cabang"
        }, {
            "title": "Region",
            "data": "regional"
        }, {
            "title": "Task ID",
            "data": "taskID"
        }, {
            "title": "Jenis Task",
            "data": "jenisTask"
        }, {
            "title": "Cust ID",
            "data": "customerID"
        }, {
            "title": "Customer Name",
            "data": "customerName"
        }, {
            "title": "Distributed Date",
            "data": "distributedDT"
        }, {
            "title": "Started Date",
            "data": "startedDT"
        }, {
            "title": "SLA Remaining",
            "data": "slaRemaining"
        }, {
            "title": "Field Person Name",
            "data": "fieldPersonName"
        }, {
            "title": "Emp Position",
            "data": "empPosition"
        }, {
            "title": "Status Prospek",
            "data": "statusProspek"
        }, {
            "title": "Priority Level",
            "data": "priorityLevel"
        }, {
            "title": "Aplikasi IA",
            "data": "aplikasiAI"
        }, {
            "title": "Application ID",
            "data": "applicationID"
        }, {
            "title": "Status MSS",
            "data": "statusMSS"
        }, {
            "title": "Status WISE",
            "data": "statusWISE"
        }, {
            "title": "Status Dukcapil",
            "data": "statusDukcapil"
        }, {
            "title": "SOA",
            "data": "soa"
        }, {
            "title": "Referantor 1",
            "data": "referentorName"
        }, {
            "title": "Referantor 2",
            "data": "referentorName2"
        }, {
            "title": "order ID",
            "data": "orderInID"
        }],
    columnDefs: [
        {
            targets: [6],
            render: function (data) {
                return '<a href="" class="rowClick">' + data + '</a>'
            }
        }, {
            targets: [22],
            visible: false,
            searchable: false
        }
    ]
});





function list() {
    var pRegion = $("#ddlRegion").val(),
        pFPName = $("#txtFpName").val(),
        pBranchName = $("#ddlBranch").val(),
        pEmpPosition = $("#ddlEmpPosition").val(),
        pTaskID = $("#txtTaskID").val(),
        pStatProspek = $("#ddlStsProspek").val(),
        pAppID = $("#txtAppID").val(),
        pPriorityLevel = $("#ddlPriorityLevel").val(),
        pCustName = $("#txtCustName").val(),
        pStatDukcapil = $("#ddlStatusDukcapil").val(),
        pSdate = $("#sdate").val(),
        pEdate = $("#edate").val(),
        pSourceData = $("#ddlSourceData").val()

    if (pFPName == null || pFPName == "")
        pFPName = "All";
    if (pTaskID == null || pTaskID == "")
        pTaskID = "All";
    if (pAppID == null || pAppID == "")
        pAppID = "All";
    if (pCustName == null || pCustName == "")
        pCustName = "All";
    if (pPriorityLevel == null || pPriorityLevel == "")
        pPriorityLevel = "All";
    if (pSdate == null || pSdate == "")
        pSdate = "All";
    if (pEdate == null || pEdate == "")
        pEdate = "All";

    swal({
        title: "Checking...",
        text: "Please wait",
        imageUrl: "css/jax-loader.gif",
        showConfirmButton: false,
        allowOutsideClick: false
    });
    $.ajax({
        url: 'TaskInquiryView/ListDetail',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            pRegion: $("#ddlRegion").val(),
            pFPName: pFPName,
            pBranchName: $("#ddlBranch").val(),
            pEmpPosition: $("#ddlEmpPosition").val(),
            pTaskID: pTaskID,
            pStatProspek: $("#ddlStsProspek").val(),
            pAppID: pAppID,
            pPriorityLevel: pPriorityLevel,
            pCustName: pCustName,
            pStatDukcapil: $("#ddlStatusDukcapil").val(),
            pSdate: pSdate,
            pEdate: pEdate,
            pSourceData: $("#ddlSourceData").val(),
            pEmpNo: $("#empNo").val()
        },
        success: function (result) {
            var jsonString = JSON.stringify(result);
            myTable.clear();
            $.each(result, function (index, value) {
                myTable.row.add(value);
            });
            myTable.draw();
            swal({
                title: "Finished!",
                showConfirmButton: false,
                timer: 500
            });
        }
    });
}

$('#myTableList tbody').on('click', '.rowClick', function (e) {
    e.preventDefault();
    var param = "";
    $.ajax({
        url: 'TaskInquiry/DecriptUser',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            Id: myTable.row(row).data().orderInID,
            userName: $("#empName").val()
        },
        success: function (result) {
            param = result;
        }
    });
    var row = $(this).closest('tr');
    var href = '';
    href = 'Taskinquiry/Views?Id=' + param
    window.open(href, '_blank');
});

$('#btnSearch').click(function (e) {
    e.preventDefault();
    if ($("#sdate").val() == "") {
        swal("Information", "Distributed Date >=  Date harus diisi.", "info")
        return false;
    }
    if ($("#edate").val() == "") {
        swal("Information", "Distributed Date <=  harus diisi.", "info")
        return false;
    }
    list();
});

$('#sdate').change(function (e) {
    e.preventDefault();
    var sdate = new Date(Date.parse($("#sdate").val()));
    var edate = new Date(Date.parse($("#edate").val()));
    if (sdate > edate) {
        swal("Information", "Distributed Date >= tidak dapat diisi > dari Distributed Date <=", "info")
        $("#sdate").val("");
        return false;
    }
});

$('#edate').change(function (e) {
    e.preventDefault();
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    var today = year + "-" + month + "-" + day;
    var sdate = new Date(Date.parse($("#sdate").val()));
    var edate = new Date(Date.parse($("#edate").val()));
    if (edate < sdate) {
        swal("Information", "Distributed Date <=  tidak dapat diisi < dari  Distributed Date >= ", "info")
        $("#edate").val(today);
        return false;
    }
});



$('#btnReset').click(function (e) {
    e.preventDefault();
    $("#ddlRegion").val("All");
    $("#txtFpName").val("");
    $("#ddlBranch").val("All");
    $("#ddlEmpPosition").val("All");
    $("#txtTaskID").val("");
    $("#ddlStsProspek").val("All");
    $("#txtAppID").val("");
    $("#ddlPriorityLevel option:selected").prop("selected", false);
    $('#ddlPriorityLevel').multiselect('rebuild');
    $("#txtCustName").val("");
    $("#ddlStatusDukcapil").val("All");
    $("#sdate").val("");
    $("#edate").val("");
    $("#ddlSourceData").val("All");
    myTable.clear();
    myTable.draw();
    var psource = "All";
    var pemp = "All";
    var pprospec = "All";
    $.ajax({
        url: 'TaskInquiry/DdlPriorityLvl',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            source: psource,
            empPost: pemp,
            prospect: pprospec
        },
        success: function (result) {
            var jsonString = JSON.stringify(result);
            $('#ddlPriorityLevel  option').each(function (index, option) {
                $(option).remove();
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
            $.each(result, function (i, a) {
                $('#ddlPriorityLevel').append($('<option>', {
                    value: result[i].value,
                    text: result[i].text
                }));
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
        }
    });
});

$(document).on("change", "#ddlRegion", function () {
    var selectedRegion = $(this).val();
    if ($("#ddlRegion").val() =="All") {
        $('#ddlBranch option').show();
        $("#ddlBranch").val("All");
    }
    else {
        $("#ddlBranch").find("option").each(function () {
            if ($(this).data("region") == selectedRegion) {
                $(this).show();
                $('#ddlBranch [data-region="0"]').show()
                $("#ddlBranch").val("All");
            } else {
                if ($("#ddlRegion").val() == "All") {
                } else {
                    $(this).hide();
                }
            }
        });
    }
    
});

$(document).on("change", "#ddlSourceData", function () {
    var selectedSource = $(this).val();
    var psource = selectedSource;
    var pemp = $("#ddlEmpPosition").val();
    var pprospec = $("#ddlStsProspek").val();
    if (psource == "")
        psource = "All"
    if (pemp == "")
        pemp = "All"
    if (pprospec == "")
        pprospec = "All"
    $.ajax({
        url: 'TaskInquiry/DdlPriorityLvl',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            source: psource,
            empPost: pemp,
            prospect: pprospec
        },
        success: function (result) {
            var jsonString = JSON.stringify(result);
            $('#ddlPriorityLevel  option').each(function (index, option) {
                $(option).remove();
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
            $.each(result, function (i, a) {
                $('#ddlPriorityLevel').append($('<option>', {
                    value: result[i].value,
                    text: result[i].text
                }));
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
        }
    });

});

$(document).on("change", "#ddlEmpPosition", function () {
    debugger;
    var selectedSource = $(this).val();
    var psource = $("#ddlSourceData").val();;
    var pemp = selectedSource;
    var pprospec = $("#ddlStsProspek").val();
    if (psource == "")
        psource = "All"
    if (pemp == "")
        pemp = "All"
    if (pprospec == "")
        pprospec = "All"
    $.ajax({
        url: 'TaskInquiry/DdlPriorityLvl',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            source: psource,
            empPost: pemp,
            prospect: pprospec
        },
        success: function (result) {
            var jsonString = JSON.stringify(result);
            $('#ddlPriorityLevel  option').each(function (index, option) {
                $(option).remove();
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
            $.each(result, function (i, a) {
                $('#ddlPriorityLevel').append($('<option>', {
                    value: result[i].value,
                    text: result[i].text
                }));
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
        }
    });

});

$(document).on("change", "#ddlStsProspek", function () {
    debugger;
    var selectedSource = $(this).val();
    var psource = $("#ddlSourceData").val();
    var pemp = $("#ddlEmpPosition").val();
    var pprospec = selectedSource;
    if (psource == "")
        psource = "All"
    if (pemp == "")
        pemp = "All"
    if (pprospec == "")
        pprospec = "All"
    $.ajax({
        url: 'TaskInquiry/DdlPriorityLvl',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            source: psource,
            empPost: pemp,
            prospect: pprospec
        },
        success: function (result) {
            var jsonString = JSON.stringify(result);
            $('#ddlPriorityLevel  option').each(function (index, option) {
                $(option).remove();
            });
            //alert(result.Value);
            $('#ddlPriorityLevel ').multiselect('rebuild');
            $.each(result, function (i, a) {
                $('#ddlPriorityLevel').append($('<option>', {
                    value: result[i].value,
                    text: result[i].text
                }));
            });
            $('#ddlPriorityLevel ').multiselect('rebuild');
        }
    });

});
