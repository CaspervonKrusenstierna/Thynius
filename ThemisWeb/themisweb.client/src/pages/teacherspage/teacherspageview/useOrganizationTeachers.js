import { useEffect, useState } from "react";
import { getOrganizationTeachers } from "../../../shared/network/organization";

export default function useOrganizationTeachers(){
    const [organizationTeachers, setOrganizationTeachers] = useState();
    useEffect(() => {
        getOrganizationTeachers().then(s => s.json()).then(s => setOrganizationTeachers(s))
    }, [])
    return [organizationTeachers, setOrganizationTeachers];
}