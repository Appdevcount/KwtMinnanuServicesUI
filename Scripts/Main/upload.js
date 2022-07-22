$(document).ready(function () {
    $("#Selectdocumenttype").click(function () {
        var value = $("#dtypecode").val();
        if (value != "") {
            $('#' + value).prop('checked', true);
        }
        $("#loginModal").modal('show');
    });

    $(".btnHideModal").click(function () {
        $("#loginModal").modal('hide');
    });
});
//fileChange(event, docType, docName)
//{
//    alert(docType);
//};
var uploadField = document.getElementById("file");

uploadField.onchange = function () {
    if (this.files[0].size >= 1000000) {
        //var fsize=this.files[0].size;
        //var aa=Math.round(fsize / 1024);
        alert("Uploaded file size greater than 1Mb");
    };
};
function documettype() {
    var radios = document.getElementsByName('ossm');

    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {

            // do whatever you want with the checked radio
            var selector = 'label[for=' + radios[i].id + ']';
            var label = document.querySelector(selector);
            var text = label.innerHTML;
            document.getElementById('dtypeid').value = radios[i].value;
            document.getElementById('dtypecode').value = radios[i].id;
            document.getElementById('inputuploadtype').value = text;
            document.getElementById('lblfile').style.backgroundColor = "#BD3B2E";
            document.getElementById('file').disabled = false;
            document.getElementById('spandtype').innerHTML = text;
            // only one radio can be logically checked, don't check the rest
        }
    }
}
$(document).ready(function (e) {
    $('#file').change(function () {
        // alert('hai');
        if ($('#file').val() == '') {
            alert('Please select file');
            return;
        }
        var data = new FormData();
        var DocumentName = document.getElementById("inputuploadtype").value;
        var DocumentType = document.getElementById('dtypeid').value;
        var files = $("#file").get(0).files;

        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("Images", files[0]);
        }
        data.append("tokenId", TID);
        data.append("mUserid", UID);
        data.append("OrgReqId", reqid);
        data.append("DocumentName", DocumentName);
        data.append("DocumentType", DocumentType);
        // Make Ajax request with the contentType = false, and procesDate = false

        var UploadDocsURL = 'http://10.10.26.226/API/api/Docs/Upload';
        //var UploadDocsURL = 'https://eservices.kgac.gov.kw/eServicesAPI/api/Docs/Upload';

        $.ajax({
            type: "POST",
            url: UploadDocsURL,
            contentType: false,
            processData: false,
            dataType: 'json',
            data: data,
            success: function (responseData) {
                var newformdata = new FormData();
                $.each(responseData.Data.FileUploadResult, function (i, item) {
                    newformdata.append("DocumentName", item.DocumentName);
                    newformdata.append("DocumentType", item.DocumentType);
                    newformdata.append("DocumentTypeCode", item.DocumentTypeCode);
                    newformdata.append("OrganizationRequestDocumentId", item.OrganizationRequestDocumentId);
                    newformdata.append("OrganizationRequestId", item.OrganizationRequestId);
                    newformdata.append("TableName", item.TableName);
                    var docid = item.OrganizationRequestDocumentId;
                    var str ='@Url.Action("UploadDocument1", "User", new { OrgReqDocId = "aa" })';
                    var res = str.replace(/aa/g, docid);
                    var url1 = "location.href='" + res + "'";
                    //    alert(res);
                    $("#idattach").append("<br /><table style='width:80%'><tr><td><span>&#128206;</span></td><td align='left'><span>" + item.DocumentName + "</span></td><td align='right'><i onclick=" + url1 + " class='glyphicon glyphicon-trash' style='border:none;color: red;'></i></td></tr></table>");
                });

                $.ajax({
                    type: "POST",
                    url: "/User/UploadDocument",
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: newformdata,
                    success: function (responseData) {
                        $('#file').val('');
                        // alert('success');
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
                //$('#idattach').html(responseData);
                //  alert(result.FileUploadResult.DocumentName);
                //document.getElementById("idattach").innerHTML = "hai";
            },
            error: function (err) {
                alert(err.statusText);
                //  document.getElementById("idattach").innerHTML=err;
            }
        });
    });
});