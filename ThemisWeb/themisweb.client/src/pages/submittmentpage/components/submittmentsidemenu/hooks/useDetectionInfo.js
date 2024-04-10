import { useEffect, useState } from "react";

export default function useDetectionInfo(detectionInfoUrl){
    return {momentsOfInterest: [{reasonOfInterest: "PASTE", index: 11}, {reasonOfInterest: "PASTE", index: 12}, {reasonOfInterest: "PASTE", index: 13}], statistics: [{infoType: "AI detektionsvärde", info: 3}, {infoType: "Genomsnittlig skrivhastighet", info: 6}]};
    const [detectionInfo, setDetectionInfo] = useState();
    useEffect(() => {

    }, [detectionInfoUrl])
    return detectionInfo;
}