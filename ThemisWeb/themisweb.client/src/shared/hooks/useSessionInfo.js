/* eslint-disable react-hooks/rules-of-hooks */
import useFetch from "./useFetch";
import React, { useEffect, useState } from "react";

export default function useSessionInfo() {
    const [sessionInfo, setSessionInfo] = useState();

    useEffect(() => {
        useFetch("/account/getsessioninfo", "GET"
        ).then(s => s.json()).then(l => {setSessionInfo(l)});
    
    }, [])

    return sessionInfo;
}
