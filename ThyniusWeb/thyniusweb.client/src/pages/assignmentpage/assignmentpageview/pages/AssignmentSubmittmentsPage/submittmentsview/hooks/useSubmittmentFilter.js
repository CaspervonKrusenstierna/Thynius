import { useMemo } from "react";


export default function useSubmittmentFilter(submittmentsData, displaySettings, itemsPerPage) {
    const toReturn =  useMemo(() => {
        let toReturn = [];
        if(displaySettings?.filter == 0){
            toReturn = submittmentsData.pages;
        }
        else{
            for(let i = 0; submittmentsData.pages.length > i; i++){
                if(submittmentsData.pages[i].WarningLevel ==  displaySettings?.filter-1){
                    toReturn.push(submittmentsData.pages[i]);
                }
            }
        }
        let temp = [];
        if(displaySettings.search != ""){
            for(let i = 0; toReturn.length > i; i++){
                if(toReturn[i].UserName.toLowerCase().startsWith(displaySettings.search.toLowerCase())){
                    temp.push(toReturn[i]);
                }
            }
            toReturn = temp;
        }
        return {pages: toReturn, dataEndPage: Math.ceil(toReturn.length / itemsPerPage) };
    }, [displaySettings.filter, displaySettings.search, submittmentsData.pages])

    return toReturn;
}
