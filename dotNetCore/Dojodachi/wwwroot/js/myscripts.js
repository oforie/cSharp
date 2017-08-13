function makeChange(){
     $.get("/update", function(res){
        console.log("This is the data i am sending your way", res), "Json"
    });
}


$(document).ready(function(){
    // alert("Hello");

    $(".action").click(function(){
        makeChange();
        
    });

});