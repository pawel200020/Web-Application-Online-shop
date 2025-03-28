"use client"
import React, {startTransition, useEffect, useState} from "react";
import styles from "./LanguageSelector.module.scss";
// @ts-ignore-next-line
import {Locale, useLocale} from "next-intl";
import {usePathname, useRouter} from '@/i18n/navigation';
import {useParams} from "next/navigation";

interface FlagIconProps {
    countryCode: string;
}

function FlagIcon({countryCode = ""}: FlagIconProps) {

    if (countryCode === "en") {
        countryCode = "gb";
    }

    return (
        <span
            className={`fi fis ${styles.fiCircle} inline-block ml-0.9 mt-0.5 fi-${countryCode}`}
        />
    );
}

interface Language {
    key: string;
    name: string;
}

export const LanguageSelector = () => {
    const router = useRouter();
    const pathname = usePathname();
    const params = useParams();
    const [languages, setLanguages] = useState<Language[]>([]);
    const locale = useLocale();

    function onSelectChange(language: string) {

        const nextLocale = language as Locale;
        startTransition(() => {
            router.replace(
                // @ts-expect-error -- TypeScript will validate that only known `params`
                // are used in combination with a given `pathname`. Since the two will
                // always match for the current route, we can skip runtime checks.
                {pathname, params},
                {locale: nextLocale}
            );
        });
    }

    useEffect(() => {
        const setupLanguages = async () => {
            const appLanguages = [{key: "pl", name: "pl"}, {key: "en", name: "en"}, {key: "de", name: "de"}];
            setLanguages(appLanguages);
        };
        setupLanguages();
    }, []);

    return (
        <>
            <div className="dropdown dropdown-hover dropdown-center ">
                <div tabIndex={0} role="button" className="btn btn-ghost btn-circle avatar">
                    <div className="w-10 rounded-full">
                        <FlagIcon countryCode={locale}/>
                    </div>
                </div>
                <ul className="dropdown-content menu bg-base-100 rounded-box left-1/2 z-1 w-13 p-2 -translate-x-1/2 shadow-xl">
                    {languages.map((language,_) => {
                        // eslint-disable-next-line react/jsx-key
                        return <li key={language.key} onClick={() => onSelectChange(language.key)}><a><FlagIcon
                            countryCode={language.key}/></a></li>
                    })}
                </ul>
            </div>
        </>
    );
}
//mt-3 - align with navbar