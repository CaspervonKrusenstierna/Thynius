import { useMemo } from "react";
import { useTranslation } from "react-i18next";

export default function useManageGroupTabs(create, groupId) {
     const [t, i18n] = useTranslation();
    let stringToInsert = (create ? "creategroup" : "editgroup") + (groupId ? `/${groupId}` : "");
    const GroupTabs = useMemo(() => {
        return [{link: `/dashboard/${stringToInsert}/general`, name: t("Overview")}, {link: `/dashboard/${stringToInsert}/members`, name: t("Members")}]
      }, [stringToInsert]);
    return GroupTabs;
}
