import { useEffect, useRef, useState } from "react";
import useFetch from "./useFetch";


export default function useFetchPaginatedData(url, currentPage, pageSize) {
    const [data, setData] = useState({dataEndPage: null, pages: []});
    const hasFetched = useRef(false);
    const preventInitialMultiFetch = useRef(false);

    useEffect(() => {
        function resolveResponse(responseData, fetchSize, pageFetching){
            let pages = [...data.pages, ...responseData];
            if(responseData.length != fetchSize){
                let dataEndPage = (pageFetching-1)+Math.floor(responseData.length/pageSize)
                hasFetched.current = true;
                setData({dataEndPage: dataEndPage, pages: pages});
            }
            else{
                setData({dataEndPage: null, pages: pages});
            }
        }
        if(hasFetched.current == false && !preventInitialMultiFetch.current){
            preventInitialMultiFetch.current = true;
            useFetch(url+"&pageIndex="+1+"&pageSize="+pageSize*3, "GET").then(s => s.json()).then(s => resolveResponse(s, pageSize*3, 2)); // fetch 3 pages initially
        }
        else if(data.dataEndPage == null && (currentPage*pageSize >= data.pages.length-pageSize*2) && hasFetched.current == true){
            console.log("hello");
            useFetch(url+"&pageIndex="+(currentPage+2)+"&pageSize="+pageSize, "GET").then(s => s.json()).then(s => resolveResponse(s, pageSize, currentPage+2)); // fetch 3 pages initially
        }
    }, [currentPage])

    return data;
}
