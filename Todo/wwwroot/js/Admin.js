var bts = document.querySelectorAll(".delete");
var buttons = document.querySelectorAll(".update");
var modal = document.getElementById("MyModal");
var span = document.getElementById("Close");
var Confirm = document.getElementById("confirmLogOut");
var cancel = document.getElementById("cancel");
var level = document.getElementById("level");
var employee = document.getElementById("employee");
var todo = document.getElementById("todoItem");
var add = document.getElementById("add");
var addItem = document.getElementById("addItem");
var addLevel = document.getElementById("addLevel");
var AddEmployee = document.getElementById("AddEmployee");
var createOn = document.getElementById("createOn");
var deleteOn = document.getElementById("deleteOn");
var UpdateCreateOn = document.getElementById("UpdateCreateOn");
var UpdateDeleteOn = document.getElementById("UpdateDeleteOn");
var id;

bts.forEach((button, index) => {
    button.onclick = function (){
        var id = button.getAttribute("data-productId");
        $.ajax({
            url: `/Admin/Delete/`,
            data:{
                id: parseInt(id),
            },
            success: function (){
                window.location.reload();
            }
        })
       
    }
})

buttons.forEach(button => {
    button.onclick = function (){
        modal.style.display = "block";
        id = button.getAttribute("data-NoteId");
        $.ajax({
            url : `/Admin/GetNote/`,
            data: {
                id: parseInt(id)
            },
            success: function (data){
                console.log(data);
                todo.value = data.name;
            }
        })
    }
})

span.onclick = function (){
    modal.style.display = "none";
}

cancel.onclick = function (){
    modal.style.display = "none";
}

Confirm.onclick = function (){
    
    $.ajax({
        url: "/Admin/UpdateNotes",
        type: "POST",
        data: {
            id: parseInt(id),
            todoItem: todo.value,
            level: parseInt(level.value),
            userName: employee.value,
            createOn: UpdateCreateOn.value,
            deleteOn: UpdateDeleteOn.value,
        },
        success: function (){
           window.location.reload();
        }
    })
}

add.onclick = function (){
    $.ajax({
        url: `/Admin/CreateNote`,
        data: {
            todoItem: addItem.value,
            level: parseInt(addLevel.value),
            userName: AddEmployee.value,
            createOn: createOn.value,
            deleteOn: deleteOn.value,
        },
        success: function (){
            window.location.reload();
        }
    })
}