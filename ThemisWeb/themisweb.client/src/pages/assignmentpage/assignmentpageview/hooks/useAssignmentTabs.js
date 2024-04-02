import { useContext, useMemo } from "react";
import { sessionInfoContext } from "../../../../App";

export default function useAssignmentTabs(assignmentId, locationState) {
    const sessionInfo = useContext(sessionInfoContext).sessionInfo;
    const GroupTabs = useMemo(() => {
        if(sessionInfo?.RoleLevel >= 1){
            return [{link:`/dashboard/assignment/${assignmentId}/description`, name:"Beskrivning"},{link:`/dashboard/assignment/${assignmentId}/submittments`, name:"Inlämningar"},{link:`/dashboard/assignment/${assignmentId}/notsubmitted`, name:"Ej Inlämnade"}]
        }
        return [{link: `/dashboard/assignment/${assignmentId}/description`, name:"Beskrivning"}]
        
      }, [assignmentId, sessionInfo]);
    return GroupTabs;
}
