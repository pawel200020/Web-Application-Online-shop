"use client";
import {Link} from '@/i18n/navigation';
import ThemeSwitcher from "@/app/context/ThemeSwitcher";
import {ThemeContext} from "@/app/context/ThemeContext";
import {useContext} from "react";
import {useTranslations} from 'next-intl';
import {LanguageSelector} from "@/app/[locale]/components/LanguageSelector";

export function Navbar() {
    const {changeTheme} = useContext(ThemeContext);
    const translations = useTranslations('Navbar');
    return (<>
        <div className="navbar bg-base-300 rounded-box shadow-xl">
            <div className="navbar-start">
                <div className="dropdown">
                    <div tabIndex={0} role="button" className="btn btn-ghost lg:hidden">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24"
                             stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                                  d="M4 6h16M4 12h8m-8 6h16"/>
                        </svg>
                    </div>
                    <ul tabIndex={1}
                        className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                        <li><Link href="/" className='link'>Main page</Link></li>
                        <li>
                            <a>Parent</a>
                            <ul className="">
                                <li><a>Submenu 1</a></li>
                                <li><a>Submenu 2</a></li>
                            </ul>
                        </li>
                        <li><a>Item 3</a></li>
                    </ul>
                </div>
                <Link className="btn btn-ghost text-xl" href={"/"}>Krakowska Linia Muzealna</Link>
            </div>
            <div className="navbar-center hidden lg:flex">
                <ul className="menu menu-horizontal px-1">
                    <li><Link href={"/"}>Item 1</Link></li>
                    <li>
                        <details>
                            <summary>Parent</summary>
                            <ul className="p-2">
                                <li><a>Submenu 1</a></li>
                                <li><a>Submenu 2</a></li>
                            </ul>
                        </details>
                    </li>
                    <li><a>Item 3</a></li>
                </ul>
            </div>

            <div className="navbar-end">
                <div className="flex-none mr-3"><LanguageSelector/></div>
                <ThemeSwitcher handleOnClick={changeTheme}/>
                <div className="flex-none">
                    <ul className="menu menu-horizontal px-1">
                        <li><Link href={"/login/"}>{[translations("login")]}</Link></li>
                        <li><Link href={"/register/"}>{[translations("register")]}</Link></li>
                    </ul>
                </div>
            </div>
        </div>
    </>)
}