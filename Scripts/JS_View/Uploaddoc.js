var applicationUrl = 'http://10.10.26.226/eServices/';



var apiUrl = 'http://10.10.26.226/API/';

//var applicationUrl = 'https://eservices.kgac.gov.kw/';



//var apiUrl = 'https://eservices.kgac.gov.kw/eServicesAPI/';


$("#bookform").submit(function (e) {
        $("#myLoadingElement").show();
    });
    $(".btn").click(function (e) {
        $("#myLoadingElement").show();
    });
$(document).ready(function () {
      
        if (ViewBagmodelstatus != null) {
            if (ViewBagmodelstatus == "6") {
                $("#MSGModalUpload").modal('show');
            }
            if (ViewBagmodelstatus == "5") {
                $("#modelcommon").modal('show');
            }
        }
        $(".btnhide").click(function () {
            $("#MSGModalUpload").modal('hide');
            $("#myLoadingElement").hide();
        });
    });

 $(document).ready(function () {
       
        if (result=="1") {
            $("#Selectdocumenttype").click(function () {
                var value = $("#dtypecode").val();
                if (value != "") {
                    $('#' + value).prop('checked', true);
                }
                $("#DocumenttypesModal").modal('show');
            });

            $(".btnhide").click(function () {
                $("#DocumenttypesModal").modal('hide');
                $("#myLoadingElement").hide();
            });
        }
    });

  var uploadField = document.getElementById("file");
    function documettype1() {
        var radios = document.getElementsByName('ossm');

        for (var i = 0, length = radios.length; i < length; i++) {
            if (radios[i].checked) {

                var selector = 'label[for=' + radios[i].id + ']';
                var label = document.querySelector(selector);
                var text = label.innerHTML;
                document.getElementById('dtypeid').value = radios[i].value;
                document.getElementById('dtypecode').value = radios[i].id;
                document.getElementById('inputuploadtype').value = text;
                document.getElementById('lblfile').style.backgroundColor = "#BD3B2E";
                document.getElementById('file').disabled = false;
                document.getElementById('spandtype').innerHTML = text;
            }
        }
    }



  $(document).ready(function (e) {
      $('#file').change(function () {
          //alert('changedfile');
            if ($('#file').val() == '') {
                return;
            }
            var data = new FormData();
            var DocumentName = document.getElementById("inputuploadtype").value;
            var DocumentType = document.getElementById('dtypeid').value;
            var UploadedFrom = "OrganizationRequest";
            var files = $("#file").get(0).files;

            if (files.length > 0) {
                data.append("Images", files[0]);
            }
            data.append("tokenId", Tid);
            data.append("mUserid", uid);
            data.append("OrgReqId", reqid1);
            data.append("DocumentName", DocumentName);
            data.append("DocumentType", DocumentType);
            data.append("UploadedFrom", UploadedFrom);
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
                        //var str = '@Url.Action("UploadDocument1", "User", new { OrgReqDocId = "aa" })';
                        var str = applicationUrl + 'User/UploadDocument1?OrgReqDocId=aa';//'/Etrade/User/UploadDocument1?OrgReqDocId=aa';
                        var res = str.replace(/aa/g, docid);
                        var url1 = "location.href='" + res + "'";

                        $("#idattach").append("<br /><table style='width:90%'><tr><td style='width:5%' align='" + UploadDocument_align_style + "'><span>&#128206;</span></td><td style='width:90%' align='" + UploadDocument_align_style + "'><span>" + item.DocumentName + "</span></td><td  style='width:5%' align='center'><i onclick=" + url1 + " class='glyphicon glyphicon-trash' style='border:none;color: red;'></i></td></tr></table>");
                    });

                    $.ajax({
                        type: "POST",
                        url: applicationUrl+'user/UploadDocument',// UploadDocs_actioncall,
                        contentType: false,
                        processData: false,
                        dataType: 'json',
                        data: newformdata,
                        success: function (responseData) {
                            //alert('successinner')
                            $('#file').val('');
                        },
                        error: function (err) {
                            alert('innererror');
                            alert(err.statusText);
                        }
                    });
                    $("#myLoadingElement").hide();
                },
                error: function (err) {
                    alert('outererror');
                    alert(err.statusText);
                }
            });
        });
    });


 $(document).ready(function () {
      
        if (result=="0") {
            //$("#mydiv :input").attr("disabled", true);
            //$("#mydiv :input").attr("style", "background-color:#ccc;width:100%;");
            $('#btncancel').prop('disabled', false);
            $('#btnprintOrg').prop('disabled', false);
            //$('.btn').attr("style", "background-color:#BD3B2E;");
     }



     $("#btnHide").click(function () {
         window.location.href = "/eservice/User/Org_RequestStatus";
     });
    });