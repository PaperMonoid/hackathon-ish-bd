--
-- PostgreSQL database dump
--

-- Dumped from database version 10.6
-- Dumped by pg_dump version 10.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: TH_acceso; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TH_acceso" (
    "I_seq" integer NOT NULL,
    "ID_usuario" integer NOT NULL,
    "Fecha_acceso" timestamp without time zone NOT NULL
);


ALTER TABLE public."TH_acceso" OWNER TO postgres;

--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TH_acceso_I_seq_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TH_acceso_I_seq_seq" OWNER TO postgres;

--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."TH_acceso_I_seq_seq" OWNED BY public."TH_acceso"."I_seq";


--
-- Name: TH_clave; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TH_clave" (
    "I_seq" integer NOT NULL,
    "ID_usuario" integer NOT NULL,
    "Clave_ant" character varying NOT NULL,
    "Fecha" timestamp without time zone NOT NULL
);


ALTER TABLE public."TH_clave" OWNER TO postgres;

--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TH_clave_I_seq_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TH_clave_I_seq_seq" OWNER TO postgres;

--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."TH_clave_I_seq_seq" OWNED BY public."TH_clave"."I_seq";


--
-- Name: T_CALIFICACIONES; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."T_CALIFICACIONES" (
    "ID_calificaciones" integer NOT NULL,
    "ID_maestro" integer NOT NULL,
    "ID_alumno" integer NOT NULL,
    "Calificacion" integer NOT NULL,
    "B_Final" boolean NOT NULL,
    "Fecha_Final" timestamp without time zone
);


ALTER TABLE public."T_CALIFICACIONES" OWNER TO postgres;

--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."T_CALIFICACIONES_ID_calificaciones_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."T_CALIFICACIONES_ID_calificaciones_seq" OWNER TO postgres;

--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."T_CALIFICACIONES_ID_calificaciones_seq" OWNED BY public."T_CALIFICACIONES"."ID_calificaciones";


--
-- Name: T_usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."T_usuarios" (
    "Nombre" character varying NOT NULL,
    "Apellido" character varying NOT NULL,
    "Clave" character varying NOT NULL,
    "Sexo" "char" NOT NULL,
    "Rol" integer NOT NULL,
    "Fecha_registro" timestamp without time zone NOT NULL,
    "Fecha_acceso" timestamp without time zone,
    "B_activo" boolean NOT NULL,
    "ID_usuario" integer NOT NULL
);


ALTER TABLE public."T_usuarios" OWNER TO postgres;

--
-- Name: TH_acceso I_seq; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_acceso" ALTER COLUMN "I_seq" SET DEFAULT nextval('public."TH_acceso_I_seq_seq"'::regclass);


--
-- Name: TH_clave I_seq; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_clave" ALTER COLUMN "I_seq" SET DEFAULT nextval('public."TH_clave_I_seq_seq"'::regclass);


--
-- Name: T_CALIFICACIONES ID_calificaciones; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES" ALTER COLUMN "ID_calificaciones" SET DEFAULT nextval('public."T_CALIFICACIONES_ID_calificaciones_seq"'::regclass);


--
-- Data for Name: TH_acceso; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TH_acceso" ("I_seq", "ID_usuario", "Fecha_acceso") FROM stdin;
\.


--
-- Data for Name: TH_clave; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TH_clave" ("I_seq", "ID_usuario", "Clave_ant", "Fecha") FROM stdin;
\.


--
-- Data for Name: T_CALIFICACIONES; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."T_CALIFICACIONES" ("ID_calificaciones", "ID_maestro", "ID_alumno", "Calificacion", "B_Final", "Fecha_Final") FROM stdin;
\.


--
-- Data for Name: T_usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."T_usuarios" ("Nombre", "Apellido", "Clave", "Sexo", "Rol", "Fecha_registro", "Fecha_acceso", "B_activo", "ID_usuario") FROM stdin;
\.


--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TH_acceso_I_seq_seq"', 1, false);


--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TH_clave_I_seq_seq"', 1, false);


--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."T_CALIFICACIONES_ID_calificaciones_seq"', 1, false);


--
-- Name: TH_acceso TH_acceso_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_acceso"
    ADD CONSTRAINT "TH_acceso_pkey" PRIMARY KEY ("I_seq");


--
-- Name: TH_clave TH_clave_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_clave"
    ADD CONSTRAINT "TH_clave_pkey" PRIMARY KEY ("I_seq");


--
-- Name: T_CALIFICACIONES T_CALIFICACIONES_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES"
    ADD CONSTRAINT "T_CALIFICACIONES_pkey" PRIMARY KEY ("ID_calificaciones");


--
-- Name: T_usuarios T_usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_usuarios"
    ADD CONSTRAINT "T_usuarios_pkey" PRIMARY KEY ("ID_usuario");


--
-- PostgreSQL database dump complete
--

