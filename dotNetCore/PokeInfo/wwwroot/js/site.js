// Write your Javascript code.

$(document).ready(function(){
     $.get("/getData", function(res){
         console.log("This is the pokemon results "  +  res);
         var name = res["name"];
         var weight = res[4].weight;
         var height = res[9].height;
         var type = res[6].sprites;
    if (res != null){
         $("#name").html("Name: " + name);
         $("#weight").html("Weight: " + weight);
         $("#height").html("Height: " + height);
         $("#type").html("Type: " + type)
    };
     
        console.log("This is the data i am sending your way " + res.body), "Json"
    });
        
});

