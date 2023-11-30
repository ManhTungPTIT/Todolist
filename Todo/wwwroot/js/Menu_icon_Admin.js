var items = document.querySelectorAll(".Item");

items.forEach((item, index) => {
    item.onclick = function (){
        console.log("Vao")
        if(index === 1){
            window.location.href = "/AdminIdividual/Index";
        }
        else{

            window.location.href = "/Admin/Index"
        }
    }
})