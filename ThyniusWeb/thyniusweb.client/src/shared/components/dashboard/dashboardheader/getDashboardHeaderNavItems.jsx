import { useTranslation } from "react-i18next"
import DashboardNavItem from "./components/DashboardNavItem/DashboardNavItem"

export default function getDashboardHeaderNavItems(privilegeLevel){
    const [t, i18n] = useTranslation();
    switch(privilegeLevel){
        case 3:
            return (
            <>
                <DashboardNavItem title={t("Home")} link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title={t("Organizations")} link="/dashboard/organizations"></DashboardNavItem>
            </>)

          case 2:
            return (
            <>
                <DashboardNavItem title={t("Home")} link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title={t("Teachers")} link="/dashboard/teachers"></DashboardNavItem>
            </>)

          case 1:
           return (
            <>
                <DashboardNavItem title={t("Home")} link="/dashboard/home"></DashboardNavItem>
                <DashboardNavItem title={t("Groups")} link="/dashboard/groups"></DashboardNavItem>
            </>)
          default:
            return (
                <>
                    <DashboardNavItem title={t("Home")} link="/dashboard/home"></DashboardNavItem>
                    <DashboardNavItem title={t("Groups")} link="/dashboard/groups"></DashboardNavItem>
                    <DashboardNavItem title={t("Texts")} link="/dashboard/texts"></DashboardNavItem>
                </>)
    }
}