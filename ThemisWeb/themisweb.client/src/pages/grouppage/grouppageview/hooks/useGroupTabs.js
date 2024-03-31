import { useMemo } from "react";

export default function useGroupTabs(groupId) {
    const GroupTabs = useMemo(() => {
        return [{link: `/dashboard/group/${groupId}/Home`, name:"Översikt"}, {link: `/dashboard/group/${groupId}/Assignments`, name:"Uppgifter"}]
      }, [groupId]);
    return GroupTabs;
}
