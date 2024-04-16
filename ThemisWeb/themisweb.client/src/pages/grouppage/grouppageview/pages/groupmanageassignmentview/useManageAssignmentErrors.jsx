import { useEffect, useState } from "react";


export function getManageAssignmentErrors(description, name, date, image){
    let temp = {};
    let errorCount = 0;
    if(description.length == 0){
        temp.descriptionError = "Du måste ange en beskrivning till din uppgift."
        errorCount++;
    }else if(description.length > 2000){
        temp.descriptionError = "Din beskrivning kan ej vara längre än 2000 karaktärer."
        errorCount++;
    }
    if(name.length < 6){
        temp.nameError = "Namnet på din uppgift får ej vara mindre än 6 karaktärer"
        errorCount++;
    }
    else if(name.length > 30){
        temp.nameError = "Namnet på din uppgift får ej vara längre än 30 karaktärer"
        errorCount++;
    }
    if(date == ""){
        temp.dateError = "Du måste ange ett sista inlämningsdatum till din uppgift"
        errorCount++;
    }
    if(image == ""){
        temp.imageError = "Du måste ange en bild till din uppgift"
        errorCount++;
    }
    return errorCount <= 0 ? null : temp;
}
export default function useManageAssignmentErrors(state){
    const [errors, setErrors] = useState(null);
    useEffect(() => {
        setErrors(getManageAssignmentErrors( state.description, state.name, state.date, state.image));

    }, [state.name, state.description, state.date, state.image])
    return errors;
}