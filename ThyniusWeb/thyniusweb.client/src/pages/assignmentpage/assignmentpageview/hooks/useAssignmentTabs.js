import { useContext, useMemo } from "react";
import { sessionInfoContext } from "../../../../App";
import { useTranslation } from "react-i18next";

export default function useAssignmentTabs(assignmentId, locationState) {
    const [t, i18n] = useTranslation();
    const sessionInfo = useContext(sessionInfoContext).sessionInfo;
    const GroupTabs = useMemo(() => {
        if(sessionInfo?.RoleLevel >= 1){
            return [{link:`/dashboard/assignment/${assignmentId}/description`, name:t("Description")},{link:`/dashboard/assignment/${assignmentId}/submittments`, name:t("Submittments")},{link:`/dashboard/assignment/${assignmentId}/notsubmitted`, name:t("Missing texts")}]
        }
        return [{link: `/dashboard/assignment/${assignmentId}/description`, name:"Beskrivning"}]
        
      }, [assignmentId, sessionInfo]);
    return GroupTabs;
}
