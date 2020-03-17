var statusRascunho = 1;
var CRM = CRM || {};
CRM.Token = {

    OnLoad: function (context) {

    },
    OnSave: function (context) {
        try {
            ///   
        }
        catch (error) {
            alert(error.description);
        }
    },

    nomeCampoOnChange: function () { },

    ObterToken: function () {
        var req = new XMLHttpRequest();
        req.open("POST", Xrm.Page.context.getClientUrl() + "/api/data/v9.1/sad_GeraTokenGlobal", true);
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.onreadystatechange = function () {
            if (this.readyState === 4) {
                req.onreadystatechange = null;
                if (this.status === 200)
                {
                    var results = JSON.parse(this.response);
                    Xrm.Utility.alertDialog(results);
                } else {
                    Xrm.Utility.alertDialog(this.statusText);
                }
            }
        };
        req.send();
    },

    TestToken() {
        //CRM.Token.ObterToken();

        ObterTokenAsync().then(processRepoListResponse, errorHandler);

    },

    ObterTokenAsync: function () {
        var promiseObj = new Promise(function (resolve, reject) {
            var xhr = new XMLHttpRequest();
            xhr.open("POST", Xrm.Page.context.getClientUrl() + "/api/data/v9.1/sad_GeraTokenGlobal", true);
            xhr.send();
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        var resp = xhr.responseText;
                        var respJson = JSON.parse(resp);
                        resolve(respJson);
                    } else {
                        reject(xhr.status);
                        console.log("xhr failed");
                    }
                } else {
                    console.log("xhr processing going on");
                }
            };
            console.log("request sent succesfully");
        });
        return promiseObj;
    }


};