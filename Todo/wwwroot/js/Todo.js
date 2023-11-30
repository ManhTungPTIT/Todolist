var btDeletes = document.querySelectorAll(".delete");
var btFinish = document.querySelectorAll(".finish");

btDeletes.forEach((button, index) => {
    var id = button.getAttribute("data-Id");
    button.onclick = function (){
        $.ajax({
            url: `/Todolist/UpdateStatusNote`,
            data: {
                id: id,
                status: 0,
            },
            success: function (){
                window.location.href = "/Todolist/Index";
            }
        })
    }
    
})

btFinish.forEach((button, index) => {
    var id = button.getAttribute("data-Id");
    button.onclick = function (){
        $.ajax({
            url: `/Todolist/UpdateStatusNote`,
            data: {
                id: id,
                status: 1,
            },
            success: function (){
                window.location.href = "/Todolist/Index";
            }
        })
    }
    
})