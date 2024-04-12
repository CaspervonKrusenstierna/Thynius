import { useEffect, useState } from "react";
import { getAssignmentTexts } from "../../../../../../../shared/network/text";

export default function useAssignmentSubmittments(assignmentId) {
    const [assignmentSubmittments, setAssignmentSubmittments] = useState({pages: []});
    useEffect(() => {
        getAssignmentTexts(assignmentId).then(s => s.json()).then(s => setAssignmentSubmittments({pages: s}));
    }, [assignmentId]);
    return assignmentSubmittments;
}