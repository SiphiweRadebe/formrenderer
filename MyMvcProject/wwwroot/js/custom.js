document.addEventListener("DOMContentLoaded", function () {
    var canvas = document.getElementById("signature-pad");

    if (canvas) {
        var signaturePad = new SignaturePad(canvas);

        document.getElementById("clear-signature").addEventListener("click", function () {
            signaturePad.clear();
        });

        window.saveSignature = function () {
            if (!signaturePad.isEmpty()) {
                var dataURL = signaturePad.toDataURL();
                document.getElementById("SignatureData").value = dataURL;
            }
        };
    } else {
        console.error("Signature pad canvas not found!");
    }
});
