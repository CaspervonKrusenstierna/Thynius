
import useFetch from "./useFetch";
import { useEffect, useState } from "react";

export default function useSessionInfo() {
    const [sessionInfo, setSessionInfo] = useState();
    function updateSessionInfo(){
        useFetch("/getsessioninfo", "GET").then(s => {if(s.status == 204){setSessionInfo(null);}else{return s.json()}}).then(l => {setSessionInfo(l);});
    }
    useEffect(() => {
       updateSessionInfo();
    }, [])
    

    return [sessionInfo, updateSessionInfo];
}
