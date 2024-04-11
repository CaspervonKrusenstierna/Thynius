import { useEffect, useState } from "react";
import { getAssignmentTexts } from "../../../../../../../shared/network/text";

export default function useAssignmentSubmittments(assignmentId) {
    const [assignmentSubmittments, setAssignmentSubmittments] = useState();
    useEffect(() => {
        getAssignmentTexts(assignmentId).then(s => s.json()).then(s => setAssignmentSubmittments(s));
    }, [assignmentId]);
    return assignmentSubmittments;
}