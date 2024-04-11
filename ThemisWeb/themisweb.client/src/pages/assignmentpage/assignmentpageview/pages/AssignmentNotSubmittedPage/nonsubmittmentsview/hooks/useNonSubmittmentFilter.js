import { useMemo } from "react";


export default function useNonSubmittmentFilter(nonSubmittments, displaySettings, itemsPerPage) {
    const toReturn =  useMemo(() => {
        let toReturn = [];
        if(displaySettings.search != ""){
            for(let i = 0; nonSubmittments.length > i; i++){
                if(nonSubmittments[i].UserName.toLowerCase().startsWith(displaySettings.search.toLowerCase())){
                    toReturn.push(nonSubmittments[i]);
                }
            }
        }
        return {pages: toReturn, dataEndPage: Math.ceil(toReturn.length / itemsPerPage) };
    }, [displaySettings.filter, displaySettings.search, nonSubmittments.pages])

    return toReturn;
}
