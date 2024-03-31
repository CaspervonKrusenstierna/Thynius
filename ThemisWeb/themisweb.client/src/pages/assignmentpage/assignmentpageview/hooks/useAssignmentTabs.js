import { useContext, useMemo } from "react";
import { sessionInfoContext } from "../../../../App";

export default function useAssignmentTabs(assignmentId, locationState) {
    const sessionInfo = useContext(sessionInfoContext);
    const GroupTabs = useMemo(() => {
        if(sessionInfo?.RoleLevel >= 1){
            return [{state:locationState, link:`/dashboard/assignment/${assignmentId}/description`, name:"Beskrivning"}, {state:locationState, link:`/dashboard/assignment/${assignmentId}/submittments`, name:"Inlämningar"}]
        }
        return [{state:locationState, link: `/dashboard/assignment/${assignmentId}/description`, name:"Beskrivning"}]
        
      }, [assignmentId, sessionInfo]);
    return GroupTabs;
}
