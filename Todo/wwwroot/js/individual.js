var btDetails = document.querySelectorAll(".detail");
var collapse = document.querySelectorAll(".collapse");
var breaks = document.querySelectorAll(".break");
var input = document.getElementById("breakId");

var url = window.location.pathname;

if(url.includes("Todo")){
    btDetails.forEach((button, index) => {
    button.onclick = function (){
        var id = button.getAttribute("data-noteId");
        var container = collapse[index];
        $.ajax({
            url: `/TodoIndividual/DetailNote`,
            data: {
                id: id,
            },
            success: function (data){
                console.log(data);
                var render = "";
                data.forEach(note => {
                    
                    render += `
                        <div class="collapse-item d-flex justify-content-end" style="width: 70rem">
                <i
                    class="fa-solid fa-circle-right fs-3"
                    style="color: #311f51; margin-left: 10rem; margin-top: 0.8rem">
                </i>
                <div
                    class="card card-body d-flex flex-row justify-content-between align-items-center mt-1">
                    <p style="width: 5rem;">${note.name}</p>
                    <p>${note.createOn}</p>
                    <p>${note.deleteOn}</p>
                </div>
            </div>
                    `
                })
                
                container.innerHTML = render;
            }
        })
    }
})

    breaks.forEach(button => {
    button.onclick = function (){
        var id = button.getAttribute("data-noteId");
        input.value = parseInt(id);
    }
})
}
else if(url.includes("Admin")){
    console.log(btDetails);
    btDetails.forEach((button, index) => {
        button.onclick = function (){
            var id = button.getAttribute("data-noteId");
            var container = collapse[index];
            $.ajax({
                url: `/AdminIdividual/DetailNote/`,
                data: {
                    id: id,
                },
                success: function (data){
                    console.log(data);
                    var render = "";
                    data.forEach(note => {

                        render += `
                        <div class="collapse-item d-flex justify-content-end" style="width: 70rem">
                <i
                    class="fa-solid fa-circle-right fs-3"
                    style="color: #311f51; margin-left: 10rem; margin-top: 0.8rem">
                </i>
                <div
                    class="card card-body d-flex flex-row justify-content-between align-items-center mt-1">
                    <p style="width: 5rem;">${note.name}</p>
                    <p>${note.createOn}</p>
                    <p>${note.deleteOn}</p>
                </div>
            </div>
                    `
                    })

                    container.innerHTML = render;
                }
            })
        }
    })

    breaks.forEach(button => {
        button.onclick = function (){
            var id = button.getAttribute("data-noteId");
            input.value = parseInt(id);
        }
    })
}

var btFinish = document.querySelectorAll(".finish");
btFinish.forEach((button, index) => {
    button.onclick = function (){
        var id = button.getAttribute("data-noteId");
        $.ajax({
            url: "/AdminIdividual/UpdateNote",
            data:{
                id: parseInt(id),
                status: 1,
            },
            success: function (){
                window.location.reload();
            }
        })
    }
})