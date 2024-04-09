import { useEffect, useState } from "react";

export default function useDetectionInfo(detectionInfoUrl){
    const [detectionInfo, setDetectionInfo] = useState();
    useEffect(() => {

    }, [detectionInfoUrl])
    return detectionInfo;
}