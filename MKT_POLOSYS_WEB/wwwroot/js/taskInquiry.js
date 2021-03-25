$('#ddlPriorityLevel').multiselect();

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
        },
        {
            "title":"download",
            defaultContent: '<input type="button" class="taskID" value="Download"/>'
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

    $.ajax({
        url: 'TaskInquiry/ListDetail',
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
        }
    });
}


$('#myTableList tbody').on('click', '.taskID', function (e) {
    e.preventDefault();
    var row = $(this).closest('tr');
    var id = myTable.row(row).data().orderInID;
    var href = '';
    href = 'TaskInquiry/ExcelDetail?pID=' + id + "&pEmpNo=" + $("#empNo").val()
    window.location.href = href;
});

$('#myTableList tbody').on('click', '.rowClick', function (e) {
    e.preventDefault();
    var row = $(this).closest('tr');
    var href = '';
    href = 'Taskinquiry/Views/' + myTable.row(row).data().orderInID;
    window.location.href = href;
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



$("#btnDownload").click(function (e) {
    e.preventDefault();
    if ($("#sdate").val() == "") {
        swal("Information", "Distributed Date >=  Date harus diisi.", "info")
        return false;
    }
    if ($("#edate").val() == "") {
        swal("Information", "Distributed Date <=  harus diisi.", "info")
        return false;
    }

    $.ajax({
        url: 'TaskInquiry/ValidasiDownload',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        async: false,
        dataType: 'json',
        type: 'POST',
        data: {
            pEmpNo: $("#empNo").val()
        },
        success: function (isSucceed) {
            if (isSucceed == true) {
                swal("Information", "Proses Download tidak bisa dilakukan karena belum dilakukan Upload pada proses Download sebelumnyaProses Download tidak bisa dilakukan karena belum dilakukan Upload pada proses Download sebelumnya.", "info")
                return false;
            }
            else {
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
                    pSourceData = $("#ddlSourceData").val(),
                    pEmpNo = $("#empNo").val()

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
                var href = '';
                href = 'TaskInquiry/Excel?pRegion=' + pRegion + "&pFPName="
                    + pFPName + "&pBranchName=" + pBranchName + "&pEmpPosition="
                    + pEmpPosition + "&pTaskID=" + pTaskID + "&pStatProspek="
                    + pStatProspek + "&pAppID=" + pAppID + "&pPriorityLevel="
                    + pPriorityLevel + "&pCustName=" + pCustName + "&pStatDukcapil="
                    + pStatDukcapil + "&pSdate=" + pSdate + "&pEdate="
                    + pEdate + "&pSourceData=" + pSourceData + "&pEmpNo="
                    + pEmpNo

                window.location.href = href;
            }
        }
    });
});



$('#btnReset').click(function (e) {
    e.preventDefault();
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    var today = year + "-" + month + "-" + day;
    $("#ddlRegion").val("All");
    $("#txtFpName").val("");
    $("#ddlBranch").val();
    $("#ddlEmpPosition").val("All");
    $("#txtTaskID").val("");
    $("#ddlStsProspek").val("All");
    $("#txtAppID").val("");
    $("#ddlPriorityLevel option:selected").prop("selected", false);
    $('#ddlPriorityLevel').multiselect('rebuild');
    $("#txtCustName").val("");
    $("#ddlStatusDukcapil").val("All");
    $("#sdate").val(today);
    $("#edate").val(today);
    $("#ddlSourceData").val("All");
});

$(document).on("change", "#ddlRegion", function () {
    var selectedRegion = $(this).val();
    $("#ddlBranch").find("option").each(function () {
        if ($(this).data("region") == selectedRegion) {
            $(this).show();
            $('#ddlBranch [data-region="0"]').show()
            $("#ddlBranch").val("");
        } else {
            $(this).hide();
        }
    });
});
