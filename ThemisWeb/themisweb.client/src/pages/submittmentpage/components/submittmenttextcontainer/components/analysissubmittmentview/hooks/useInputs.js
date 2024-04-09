import { useContext, useEffect, useState } from "react";
import { submittmentInfoContext } from "../../../../../SubmittmentPage";
import { parseCsv } from "../utilities/Utilities";

export default function useInputs(){
    const submittmentInfo = useContext(submittmentInfoContext);
    const [inputs, setInputs] = useState(null);
    useEffect(() => {
        async function getInputs(){
          await fetch(submittmentInfo.links.inputDataURL).then(s => s.text()).then(s => {setInputs(parseCsv(s, ",", '\n'))});
        }
        getInputs();
    }, [submittmentInfo.links])
    return inputs;
}
