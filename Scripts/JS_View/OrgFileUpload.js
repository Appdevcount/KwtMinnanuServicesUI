
var DocViewURL;
function ViewDoc(event, FileExtension) {
   // //debugger;

    console.log("docidempty");
    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(BaseUniqueID);
    var DocumentTypeId = BaseUniqueID + "DTID";
    var FileControlId = BaseUniqueID + "FC";
    var BrowseIconId = BaseUniqueID + "BI";
    var DeleteId = BaseUniqueID + "DLT";
    var DownloadId = BaseUniqueID + "DWL";
    var SmallViewId = BaseUniqueID + "SV";
    var DocumentIdField = BaseUniqueID + "DID";

    var DocumentId = document.getElementById(DocumentIdField).value;// event.target.parentElement.parentElement.firstElementChild.children[4].value;//
    if (DocumentId) {
         DocViewURL = applicationUrl + 'User/RenderDocument?DocumentId=' + DocumentId;

        if (FileExtension.indexOf('pdf') !== -1) {
            document.getElementById('DocRenderingFrame').src = DocViewURL;
            document.getElementById('DocRenderingFrame').style.display = 'block';
            document.getElementById('DocRenderingContainer').style["background-image"] = "";
        }
        else {
            document.getElementById('DocRenderingFrame').style.display = 'none';
            document.getElementById('DocRenderingFrame').src = "";
            document.getElementById('DocRenderingContainer').style["background-image"] = "url(" + DocViewURL + ")";
            document.getElementById('DocRenderingContainer').style["background-size"] = "100% 100%";
            document.getElementById('DocRenderingContainer').style["background-repeat"] = "no-repeat";
            document.getElementById('DocRenderingContainer').style["background-position"] = "center";
        }

        console.log(DocViewURL);

        $("#DocViewerModal").modal("show");


    }
    else {
        document.getElementById(FileControlId).click();

    }

}
function BrowseFile(event) {
   // //debugger;
    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(BaseUniqueID);
    var DocumentTypeId = BaseUniqueID + "DTID";
    var FileControlId = BaseUniqueID + "FC";
    var BrowseIconId = BaseUniqueID + "BI";
    var DeleteId = BaseUniqueID + "DLT";
    var DownloadId = BaseUniqueID + "DWL";
    var SmallViewId = BaseUniqueID + "SV";
    var DocumentIdField = BaseUniqueID + "DID";

    document.getElementById(FileControlId).click();
}
function ShowFileChosen(event) {
    //debugger;
    if (event.target.files && event.target.files[0]) {

        var val = event.target.files[0].name.toLowerCase();
        regex = new RegExp("(.*?)\.(jpg|jpeg|png|pdf|bmp)$");

        if (!(regex.test(val)) || event.target.files[0].size>1000000) {
            $(this).val('');
            //alert('Please select correct file format');

            //var msg = ' @Resources.Resource.UploadDocument_documentformatnote ';
            $('#msgmodalMessage').text(msgdocumentformatnote);
            $('#msgmodalDiv').modal('toggle');
            return;
        }
        

        var IDSplit = event.target.id.split("_");
        var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
        console.log(BaseUniqueID);
        var DocumentTypeId = BaseUniqueID + "DTID";
        var FileControlId = BaseUniqueID + "FC";
        var BrowseIconId = BaseUniqueID + "BI";
        var DeleteId = BaseUniqueID + "DLT";
        var DownloadId = BaseUniqueID + "DWL";
        var SmallViewId = BaseUniqueID + "SV";
        var DocumentIdField = BaseUniqueID + "DID";
        var DocNameId = BaseUniqueID + "DNID";

       

        console.log("DocNameId");
        console.log(DocNameId);
        var DocName = document.getElementById(DocNameId);

        var Exactfilename = event.target.files[0].name;
        var filename = event.target.files[0].name;
        var filenamelengthlarger = event.target.files[0].name.length < 15;
        filename = filenamelengthlarger ? event.target.files[0].name : event.target.files[0].name.substring(0, 15);
        filename = filenamelengthlarger ? filename : filename + "...";
        DocName.innerHTML = filename;////"xfgbfg";//.setAttribute('class', 'FileSelectedIcon');

        var extension = Exactfilename.substring(Exactfilename.lastIndexOf('.') + 1);

        var tmppath = URL.createObjectURL(event.target.files[0]);
        var SmallView = document.getElementById(SmallViewId);//
        console.log(SmallView);
        SmallView.setAttribute('class', 'SmallView');
        if (extension.toLowerCase().indexOf('pdf') === -1) {
            SmallView.style["background-image"] = "url(" + tmppath + ")";
        }
        else {
            SmallView.style["background-image"] = "url(" + "images/UploadDocIcons/pdficon.png" + ")";
        }

        var DocTypeId = document.getElementById(DocumentTypeId).value;
      //  //debugger;
        var DocActions = uploadFile(event, event.target.files[0], event.target.files[0].name, DocTypeId);

    }
    else {
        //var msg = '@Resources.Resource.uploadminimumonedocument';
        $('#msgmodalMessage').text(msguploadminimumonedocument);
        $('#msgmodalDiv').modal('toggle');
    }

}
function Save(event) {
   // //debugger;
    var FileControlId = event.target.id.substring(0, event.target.id.length - 1);

}
function DeleteFile(event) {
   // //debugger;

    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(BaseUniqueID);
    var DocumentTypeId = BaseUniqueID + "DTID";
    var FileControlId = BaseUniqueID + "FC";
    var BrowseIconId = BaseUniqueID + "BI";
    var DeleteId = BaseUniqueID + "DLT";
    var DownloadId = BaseUniqueID + "DWL";
    var SmallViewId = BaseUniqueID + "SV";
    var DocumentIdField = BaseUniqueID + "DID";

    //var deleteStr = applicationUrl + 'user' + '/UploadDocument1?OrgReqDocId=aa';
    var deleteStr = applicationUrl + 'user' + '/DeleteFileFromServer?OrgReqDocId=aa';
    var HiddenDocId = document.getElementById(DocumentIdField).value;// event.target.parentElement.parentElement.children[4].value;

    var resDelete = deleteStr.replace(/aa/g, HiddenDocId);
    //window.location.href = resDelete;
    DeleteFileFromServer(resDelete, event);

}
function DownloadFile(event) {
   // //debugger;

    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(BaseUniqueID);
    var DocumentTypeId = BaseUniqueID + "DTID";
    var FileControlId = BaseUniqueID + "FC";
    var BrowseIconId = BaseUniqueID + "BI";
    var DeleteId = BaseUniqueID + "DLT";
    var DownloadId = BaseUniqueID + "DWL";
    var SmallViewId = BaseUniqueID + "SV";
    var DocumentIdField = BaseUniqueID + "DID";

    var downloadStr = applicationUrl + '/BrokerRenewal/DownloadFile?filename=aa&&referenceprofile=OrganizationRequests';
    var HiddenDocId = document.getElementById(DocumentIdField).value;// event.target.parentElement.parentElement.children[4].value;

    var resDownload = downloadStr.replace(/aa/g, HiddenDocId);
    window.location.href = resDownload;


}


function AddNewFileControl(event) {
   // //debugger;

  

    var IDSplit = event.target.id.split("_");

    var ExistingBaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(ExistingBaseUniqueID);
    var DocumentTypeId = ExistingBaseUniqueID + "DTID";
    var FileControlId = ExistingBaseUniqueID + "FC";
    var BrowseIconId = ExistingBaseUniqueID + "BI";
    var DeleteId = ExistingBaseUniqueID + "DLT";
    var DownloadId = ExistingBaseUniqueID + "DWL";
    var SmallViewId = ExistingBaseUniqueID + "SV";
    var DocumentIdField = ExistingBaseUniqueID + "DID";
    var DocNameId = ExistingBaseUniqueID + "DNID";

   

    var DocumentTypeReference = IDSplit[0] + "_" + IDSplit[1];
    console.log(DocumentTypeReference);
    var oldfilenumber = IDSplit[3];
    console.log(oldfilenumber);
    var newfilenumber = ++oldfilenumber;
    console.log(newfilenumber);
    var newDocumentTypeReferencewithFileNumber = DocumentTypeReference + "_" + IDSplit[2] + "_" + newfilenumber + "_";//Complete new reference
    console.log(newDocumentTypeReferencewithFileNumber);

    var NewBaseUniqueID = newDocumentTypeReferencewithFileNumber;
    console.log(NewBaseUniqueID);
    var NewDocumentTypeId = NewBaseUniqueID + "DTID";
    var NewFileControlId = NewBaseUniqueID + "FC";
    var NewBrowseIconId = NewBaseUniqueID + "BI";
    var NewDeleteId = NewBaseUniqueID + "DLT";
    var NewDownloadId = NewBaseUniqueID + "DWL";
    var NewSmallViewId = NewBaseUniqueID + "SV";
    var NewDocumentIdField = NewBaseUniqueID + "DID";
    var NewDocNameId = NewBaseUniqueID + "DNID";

    ////dataattribute = "data-" + event.target.id.replace(/_/g, '').toLowerCase();
    //dataattribute = "data-" + (DocumentTypeReference).replace(/_/g, '').toLowerCase();
    //console.log(dataattribute);
    //var dataElems = document.querySelectorAll("[" + dataattribute + "]");
    //if (dataElems.length > 0) {
    //    //var msg = '@Resources.Resource.uploadminimumonedocument';
    //    $('#msgmodalMessage').text(msguploadminimumonedocument);
    //    $('#msgmodalDiv').modal('toggle');
    //    return;
    //}

    //dataattribute = "data-" + event.target.id.replace(/_/g, '').toLowerCase();
    dataattribute = "data-" + (DocumentTypeReference).replace(/_/g, '').toLowerCase();
    console.log(dataattribute);
    var dataElems = document.getElementsByClassName(dataattribute);
    if (dataElems.length > 0) {
        //var msg = '@Resources.Resource.uploadminimumonedocument';
        $('#msgmodalMessage').text(msguploadminimumonedocument);
        $('#msgmodalDiv').modal('toggle');
        return;
    }

    var SelectedElement = event.target;
    var newElement = findAncestor(SelectedElement, 'UploadedFileContainer');// SelectedElement.parentElement.parentElement.parentElement.parentElement.parentElement;

    var cln = newElement.cloneNode(true);
    var classlistfornewcontainer = 'col-xs-12 col-sm-6 col-md-6 col-lg-4 UploadedFileContainer ' + dataattribute;
    cln.setAttribute('class', classlistfornewcontainer);
    //cln.setAttribute(dataattribute, dataattribute);
    console.log(cln);
    console.log(cln.getElementsByClassName(DocumentTypeId));
    //cln.getElementsByTagName("input").getElementsByClassName("ad");
    cln.getElementsByClassName(ExistingBaseUniqueID)[0].id = NewBaseUniqueID;
    cln.getElementsByClassName(ExistingBaseUniqueID)[0].setAttribute('class', 'iconstyle '+NewBaseUniqueID);
    cln.getElementsByClassName(DeleteId)[0].id = NewDeleteId;
    cln.getElementsByClassName(DeleteId)[0].setAttribute('class', 'iconstyle ' +NewDeleteId);

    cln.getElementsByClassName(DocumentTypeId)[0].id = NewDocumentTypeId;
    cln.getElementsByClassName(DocumentTypeId)[0].setAttribute('class', NewDocumentTypeId);
    cln.getElementsByClassName(FileControlId)[0].id = NewFileControlId;
    cln.getElementsByClassName(FileControlId)[0].setAttribute('class', NewFileControlId);
    cln.getElementsByClassName(BrowseIconId)[0].id = NewBrowseIconId;
    cln.getElementsByClassName(BrowseIconId)[0].setAttribute('class', 'iconstyle ' +NewBrowseIconId);
    //cln.getElementsByClassName(NewBrowseIconId)[0].src = "/Images/UploadDocIcons/Browse.png";
    cln.getElementsByClassName(SmallViewId)[0].id = NewSmallViewId;
    cln.getElementsByClassName(SmallViewId)[0].setAttribute('class', NewSmallViewId);
    var docidfieldtest = cln.getElementsByClassName(DocumentIdField)[0];
    cln.getElementsByClassName(DocumentIdField)[0].setAttribute('class', NewDocumentIdField);
    cln.getElementsByClassName(NewDocumentIdField)[0].value = "";
    cln.getElementsByClassName(NewDocumentIdField)[0].id = NewDocumentIdField;
    cln.getElementsByClassName(DocNameId)[0].setAttribute('class', NewDocNameId);
    cln.getElementsByClassName(NewDocNameId)[0].innerHTML = "";
    cln.getElementsByClassName(NewDocNameId)[0].id = NewDocNameId;


    console.log("111");
    var FileBrowseIcon = cln.getElementsByClassName(NewBrowseIconId)[0];// cln.children[0].children[0].children[0].children[0].children[0];
    FileBrowseIcon.style["display"] = "block";

    console.log(cln);
    console.log(cln.children);
    var smallview = cln.getElementsByClassName(NewSmallViewId)[0];// cln.children[0].children[1].children[1].children[0];
    smallview.style["background-image"] = "";
    var classgroup = 'btn btn-default SmallView FileSearch ' + SmallViewId;
    smallview.setAttribute('class', classgroup);


    //var NewFileContainerDelete = cln.getElementsByClassName(DeleteId)[0];// cln.firstElementChild.lastElementChild.firstElementChild.children[1];
    //    NewFileContainerDelete.style["display"] = "none";

    //var NewFileContainerAdd = cln.getElementsByClassName(NewBaseUniqueID)[0];// cln.firstElementChild.lastElementChild.firstElementChild.children[1];
    //NewFileContainerAdd.style["display"] = "none";



    //cln.getElementsByClassName(ExistingBaseUniqueID)[0].src = applicationUrl + "/Images/UploadDocIcons/Delete.png";
    //cln.getElementsByClassName(ExistingBaseUniqueID)[0].setAttribute('onclick', 'DeleteContainer(event)');
    cln.getElementsByClassName(NewDeleteId)[0].src = applicationUrl + "/Images/UploadDocIcons/Delete.png";
    cln.getElementsByClassName(NewDeleteId)[0].setAttribute('onclick', 'DeleteContainer(event)');
    cln.getElementsByClassName(NewDeleteId)[0].setAttribute('style', 'margin-top: 18px;');
    
    cln.getElementsByClassName(NewBaseUniqueID)[0].src = applicationUrl + "/Images/UploadDocIcons/Add.png";
    cln.getElementsByClassName(NewBaseUniqueID)[0].setAttribute('onclick', 'AddNewFileControl(event)');
    cln.getElementsByClassName(NewBaseUniqueID)[0].setAttribute('style', 'display:none;');

    AddNewFileControlAfterInitiatedElement(cln, newElement);
}
function findAncestor(el, cls) {
    while ((el = el.parentNode) && el.className.indexOf(cls) < 0);
    return el;
}
function DeleteContainer(event) {
    var CurrentContainer = findAncestor(event.target, 'UploadedFileContainer');
    CurrentContainer.parentElement.removeChild(CurrentContainer);//IE Fix
    //CurrentContainer.remove();
}
function AddNewFileControlAfterInitiatedElement(newElement, ExistingElement) {
    var parent = ExistingElement.parentNode;

    if (parent.lastChild === ExistingElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement, ExistingElement.nextSibling);
    }
}






$().ready(function () {



    if (RedirectToOrgStatus) {
      //  //debugger;
        if (RedirectToOrgStatus === 'RedirectToOrgStatus') {
            $("#msgmodalMessage").text(msg);
            $("#msgmodalDiv").css("display", "block");

            setTimeout(function () {

                window.location.href = applicationUrl + '/Request/Requestlistfortheuser';

            }, 3000);
        }
    }

}); // End Ready




function uploadFile(event, file, documentName_, DocTypeId) {
    //debugger;

    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";
    console.log(BaseUniqueID);
    var DocumentTypeId = BaseUniqueID + "DTID";
    var FileControlId = BaseUniqueID + "FC";
    var BrowseIconId = BaseUniqueID + "BI";
    var DeleteId = BaseUniqueID + "DLT";
    var DownloadId = BaseUniqueID + "DWL";
    var SmallViewId = BaseUniqueID + "SV";
    var DocumentIdField = BaseUniqueID + "DID";
    var DocumentNameField = BaseUniqueID + "DNID";

    var dataattribute = "data-" + (IDSplit[0] + "_" + IDSplit[1]).replace(/_/g, '').toLowerCase();
    var Exactfilename = event.target.files[0].name;
    var extension = Exactfilename.substring(Exactfilename.lastIndexOf('.') + 1);

    let data = new FormData();

    let documentType = DocTypeId;

    data.append("Images", file);//fileControl.files[0]);

    let Tidd = document.getElementById("Tid").value;

    let uidd = document.getElementById("uid").value;

    let reqidd = document.getElementById("reqid1").value;

    data.append("tokenid", Tidd);
    data.append("mUserid", uidd);
    data.append("OrgReqId", reqidd);
    data.append("OrgId", 0);
    data.append("DocumentName", documentName_);
    data.append("ImportLicenseDoc", false);
    data.append("DocumentType", documentType);

    data.append("Eservicerequestid", $("#Eservicerequestid").val());

    data.append("UploadedFrom", $("#Referenceprofile").val());


    let uploadURL = apiUploadUrl;// 'http://10.10.26.226/eSOnePPIA/eServicesAPI/api/Docs/Upload';// 'http://localhost/ETradeAPI/api/Docs/Upload';


    var DocActions;

    $.ajax({
        type: "POST",
        //cache: false, // Added for Security
        url: uploadURL,
        contentType: false,
        processData: false,
        dataType: 'json',
        data: data,
        success: function (responseData) {
            ////debugger;
            ////console.log(responseData);
            ////console.log(responseData.Data.FileUploadResult[0]);
            ////console.log(responseData.Data.FileUploadResult[0].Encryptedid);
            //////alert(responseData.Data.FileUploadResult[0].Encryptedid);
            //document.getElementById(DocumentIdField).value = responseData.Data.FileUploadResult[0].Encryptedid;
            //document.getElementById(DocumentNameField).value = responseData.Data.FileUploadResult[0].DocumentName;
            //var AddIcon= document.getElementById(BaseUniqueID);
            //AddIcon.setAttribute('style', 'display:block');
            //var DeleteIcon = document.getElementById(DeleteId);
            //DeleteIcon.setAttribute('style', 'display:block;margin-top: 14px;');
            //DeleteIcon.setAttribute('onclick', 'DeleteFile(event)');

            //var FileBrowseIcon = document.getElementById(BrowseIconId) ;//
            //FileBrowseIcon.style["display"] = "none";


            //var tmppath = URL.createObjectURL(event.target.files[0]);
            //var SmallView = document.getElementById(SmallViewId);//
            //console.log(SmallView);
            //SmallView.setAttribute('class', 'SmallView ' + SmallViewId);
            //if (extension.toLowerCase().indexOf('pdf') === -1) {
            //    SmallView.style["background-image"] = "url(" + tmppath + ")";
            //    SmallView.style["cursor"] = "pointer";
            //}
            //else {
            //    SmallView.style["background-image"] = "url(" + applicationUrl + "images/UploadDocIcons/pdficon.png" + ")";
            //    SmallView.style["cursor"] = "pointer";
            //}

            //var newlyAddedcontainer = document.getElementsByClassName(dataattribute)[0];
            //newlyAddedcontainer.classList.remove(dataattribute);
            //window.location.reload();
            window.location.href = window.location.href.replace(/(\?.*)|(#.*)/g, "") + '?doctogo=' + IDSplit[0] + "_" + IDSplit[1];
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            console.log(msg);
            console.log(jqXHR);
            console.log(exception);
            //var msg = '@Resources.Resource.Someissuehasoccured';
            $('#msgmodalMessage').text(msgSomeissuehasoccured);
            $('#msgmodalDiv').modal('toggle');
        }
    });
    return DocActions;
}



function DeleteFileFromServer(DeleteUrl, event) {
   // //debugger;
    var IDSplit = event.target.id.split("_");
    var BaseUniqueID = IDSplit[0] + "_" + IDSplit[1] + "_" + IDSplit[2] + "_" + IDSplit[3] + "_";

    $.ajax({
        type: "GET",
        //cache: false, // Added for Security
        url: DeleteUrl,
        contentType: false,
        processData: false,
        dataType: 'json',
        //data: data,
        success: function (responseData) {
            //var CurrentFileComtainer = findAncestor(event.target, 'UploadedFileContainer');// SelectedElement.parentElement.parentElement.parentElement.parentElement.parentElement;
            //CurrentFileComtainer.parentElement.removeChild(CurrentFileComtainer);
            //console.log(responseData);
            window.location.href = window.location.href.replace(/(\?.*)|(#.*)/g, "") + '?doctogo=' + IDSplit[0] + "_" + IDSplit[1];
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            console.log(msg);
            console.log(jqXHR);
            console.log(exception);
            //var msg = '@Resources.Resource.Someissuehasoccured';
            $('#msgmodalMessage').text(msgSomeissuehasoccured);
            $('#msgmodalDiv').modal('toggle');
        }
    });
    
}


function qs(key) {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars[key];
}

$().ready(function () {
    //alert();
    var result = qs('doctogo');
    //alert(result);
    //console.log(result);
    //document.getElementById(result).scrollIntoView();
    $('html, body').animate({
        scrollTop: $('#' + result).offset().top
    }, 'slow');

    console.log($('#' + result).parent().parent().parent().parent());
});



        //document.addEventListener("adobe_dc_view_sdk.ready", function () {
        //var adobeDCView = new AdobeDC.View({
        //    clientId: "18b068bc582a4a0984f9263515643423",
        //    divId: "adobe-dc-view"//"DocRenderingContainer"
        //});
        //adobeDCView.previewFile({
        //    content: { location: { url: DocViewURL } },
        //    metaData: {fileName: "File.pdf" }
        //}, {
        //                embedMode: "IN_LINE",
        //            showDownloadPDF: false,
        //            showPrintPDF: false
        //        });
        //    });
