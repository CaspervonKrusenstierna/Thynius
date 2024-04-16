/* eslint-disable react-hooks/rules-of-hooks */
import useFetch from "./useFetch";
import React, { useEffect, useState } from "react";

export default function useSessionInfo() {
    const [sessionInfo, setSessionInfo] = useState();
    const [loading, setLoading] = useState(true);
    function updateSessionInfo(){
        useFetch("/getsessioninfo", "GET").then(s => {if(s.status == 401){setLoading(false); setSessionInfo(null)}else{return s.json()}}).then(l => {setLoading(false); setSessionInfo(l)});
    }
    useEffect(() => {
       updateSessionInfo();
    }, [])
    

    return [loading, sessionInfo, updateSessionInfo];
}
