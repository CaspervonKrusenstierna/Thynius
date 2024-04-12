import { useEffect, useState } from "react";
import useFetch from "../../../../../shared/hooks/useFetch";

export default function useDetectionInfo(detectionInfoUrl){
   // return {momentsOfInterest: [{reasonOfInterest: "PASTE", index: 11}, {reasonOfInterest: "PASTE", index: 12}, {reasonOfInterest: "PASTE", index: 13}], statistics: [{infoType: "AI detektionsvärde", info: 3}, {infoType: "Genomsnittlig skrivhastighet", info: 6}]};
    const [detectionInfo, setDetectionInfo] = useState();
    useEffect(() => {
        useFetch(detectionInfoUrl, "GET").then(s => s.json()).then(s => setDetectionInfo(s));
    }, [detectionInfoUrl])
    return detectionInfo;
}