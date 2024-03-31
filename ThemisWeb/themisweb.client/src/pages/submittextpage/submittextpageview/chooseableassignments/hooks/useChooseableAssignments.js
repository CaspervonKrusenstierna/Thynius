/* eslint-disable react-hooks/rules-of-hooks */

import React, { useEffect, useState } from "react";
import useFetch from "../../../../../shared/hooks/useFetch";

export default function useChooseableAssignments() {
    const [assignments, setAssignments] = useState();

    useEffect(() => {
        useFetch("/assignment/getuserassignments", "GET").then(s => s.json()).then(l => {setAssignments(l)});
    }, [])


    return assignments;
}
