$(document).ready(function () {
    var dizi;
    if (JSON.parse(localStorage.getItem('urun')) && JSON.parse(localStorage.getItem('urun')).length > 0) {
        dizi = JSON.parse(localStorage.getItem('urun'));
    } else {
        dizi = [];
    }


    $(".ekle").click(function (event) {
        //  dizi= urunler=JSON.parse(localStorage.getItem('urun'));
        let resim = $(this).parent().find('#res').prop('src');
        //console.log(resim)
        let aciklama = $(this).parent().find("#urunbilgi").text();
        //console.log(aciklama)
        let fiyat = $(this).parent().find(".fiyat").text().trim();
        //console.log(fiyat)

        toastr.success(aciklama + " ürünü başarıyla sepete eklendi!")
        event.preventDefault();
        var urunler = JSON.parse(localStorage.getItem('urun'));
        if (urunler) {
            var uaciklama = $(this).parent().find("#urunbilgi").text();
            for (var i = 0; i < urunler.length; i++) {
                if (urunler[i]["Aciklama"] === uaciklama) {
                    var defaultmiktar = parseInt(urunler[i]["Miktar"]);
                    urunler[i]["Miktar"] = defaultmiktar + 1;
                    localStorage.setItem('urun', JSON.stringify(urunler));
                    return;
                }
            }
        }

        var veriler = {
          //  UrunAd: "",
            Resim:"",
            Aciklama: "",
            Fiyat: "",
            Miktar: "1"
        }
        veriler.Aciklama = aciklama;
        veriler.Resim = resim;
        veriler.Fiyat = fiyat;
        
        dizi.push(veriler);
        localStorage.setItem('urun', JSON.stringify(dizi));
    });


    $("#sepettemizle").click(function () {
        localStorage.clear();
    });
});