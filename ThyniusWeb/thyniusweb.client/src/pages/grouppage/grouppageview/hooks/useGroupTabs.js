import { useMemo } from "react";
import { useTranslation } from "react-i18next";

export default function useGroupTabs(groupId) {
  const [t, i18n] = useTranslation();
    const GroupTabs = useMemo(() => {
        return [{link: `/dashboard/group/${groupId}/Home`, name: t("Overview")}, {link: `/dashboard/group/${groupId}/Assignments`, name:t("Assignments")}]
      }, [groupId]);
    return GroupTabs;
}
