import { useMemo } from "react";


export default function useSubmittmentFilter(paginatedData, displaySettings, itemsPerPage) {
    const toReturn =  useMemo(() => {
        let toReturn = [];
        if(displaySettings?.filter == 0){
            toReturn = paginatedData.pages;
        }
        else{
            for(let i = 0; paginatedData.pages.length > i; i++){
                if(paginatedData.pages[i].WarningLevel ==  displaySettings?.filter-1){
                    toReturn.push(paginatedData.pages[i]);
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
    }, [displaySettings.filter, displaySettings.search, paginatedData.pages])

    return toReturn;
}
