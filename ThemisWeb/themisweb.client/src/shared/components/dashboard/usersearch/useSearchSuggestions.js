import { useEffect, useState } from "react";
import useFetch from "../../../hooks/useFetch";


export default function useSearchSuggestions(search) {
    const [searchResults, setSearchResults] = useState();
    useEffect(() => {
        if(search.length > 3){
            useFetch("/users/searchusers?search=" + search, "GET").then(s => s.json()).then(s => {setSearchResults(s)});
        }
    }, [search])
    return [searchResults, setSearchResults];
}