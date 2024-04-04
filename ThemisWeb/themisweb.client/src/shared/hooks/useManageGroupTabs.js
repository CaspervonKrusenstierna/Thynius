import { useMemo } from "react";

export default function useManageGroupTabs(create, groupId) {
    let stringToInsert = (create ? "creategroup" : "editgroup") + (groupId ? `/${groupId}` : "");
    const GroupTabs = useMemo(() => {
        return [{link: `/dashboard/${stringToInsert}/general`, name:"Översikt"}, {link: `/dashboard/${stringToInsert}/members`, name:"Medlemmar"}]
      }, [stringToInsert]);
    return GroupTabs;
}
