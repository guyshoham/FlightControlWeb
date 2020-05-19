(function () {
    var dropZone = document.getElementById('myFlights');

    //handle Json file from DropZone
    var upload = function (files) {
        //file reader
        var reader = new FileReader();
        reader.readAsText(files[0]);

        //Invoked when reader reads the file
        reader.onload = function (e) {
            //stores text from json file
            var text = reader.result;
            let postOption = {
                "method": "POST",
                "headers": {
                    'Content-Type': "application/json;charset=utf-8"
                },
                "body": text
            };

            fetch("/api/FlightPlan", postOption)
                .then(response => response.json())
                .then(showSnackbar("FlightPlan Uploaded Successfully!", "success"))
                .catch(error => showSnackbar(error))
        }

    }

    dropZone.ondrop = function (e) {
        //prevent open file in browser - deafault drop behavior
        e.preventDefault();
        this.className = 'myFlights';
        upload(e.dataTransfer.files);
    }

    //mouse enter
    dropZone.ondragover = function () {
        this.className = 'myFlights dragOver';
        return false;
    }
    //mouse leave
    dropZone.ondragleave = function () {
        this.className = 'myFlights';
        return false;
    }
}());
