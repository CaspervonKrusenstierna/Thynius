/* eslint-disable react-hooks/rules-of-hooks */
import useFetch from "./useFetch";

import { useEffect, useState } from "react";
export default function useSessionInfo() {
    const [sessionInfo, setSessionInfo] = useState("");

    useEffect(() => {
        useFetch("/account/getsessioninfo", "GET"
        ).then(s => {setSessionInfo(JSON.parse(s))})
    }, [])

    return sessionInfo;
}
