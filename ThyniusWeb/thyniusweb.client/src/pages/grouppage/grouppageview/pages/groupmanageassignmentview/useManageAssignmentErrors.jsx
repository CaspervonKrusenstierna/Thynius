import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";


export function getManageAssignmentErrors(description, name, date, image, t){
    let temp = {};
    let errorCount = 0;
    if(description.length == 0){
        temp.descriptionError = t("You need to submit a description for your assignment.")
        errorCount++;
    }else if(description.length > 2000){
        temp.descriptionError =  t("Your description cannot contain more than 2000 characters.")
        errorCount++;
    }
    if(name.length < 6){
        temp.nameError = t("The name of your assignment needs to contain 5 characters at minimum.")
        errorCount++;
    }
    else if(name.length > 30){
        temp.nameError = t("The name of your assignment cannot contain more than 30 characters.")
        errorCount++;
    }
    if(date == ""){
        temp.dateError = t("You need to submit a valid deadline for your assignment.")
        errorCount++;
    }
    if(image == ""){
        temp.imageError = t("You need to submit an image for your assignment.")
        errorCount++;
    }
    return errorCount <= 0 ? null : temp;
}
export default function useManageAssignmentErrors(state){
    const [t, i18n] = useTranslation();
    const [errors, setErrors] = useState(null);
    useEffect(() => {
        setErrors(getManageAssignmentErrors( state.description, state.name, state.date, state.image, t));

    }, [state.name, state.description, state.date, state.image])
    return errors;
}