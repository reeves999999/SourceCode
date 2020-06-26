const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const dirName = 'dist';

module.exports = (env, argv) => {
    return {
        watch: true,
        mode: argv.mode === "production" ? "production" : "development",

        entry: {
            main: './src/js/sourcecode-index.js',
            clientsList: './src/js/clients-list.js',
            sourcecode: './src/sass/sourcecode-index.scss'
        },

        output: {
            filename: 'sourcecode-[name].js',
            path: path.resolve(__dirname, dirName),
            publicPath: '/dist'
        },
        module: {
            rules: [
                {
                    test: /\.(js)$/,
                    use: 'babel-loader'
                },
                {
                    test: /\.scss$/,
                    use:
                        [
                            'style-loader',
                            MiniCssExtractPlugin.loader,
                            'css-loader',
                            {
                                loader: 'postcss-loader',
                                options: {
                                    minimize: true,
                                    sourceMap: true,
                                    config: {
                                        ctx: {
                                            env: argv.mode
                                        }
                                    }
                                }
                            },
                            'sass-loader'
                        ]
                }
            ]
        },
        plugins: [
            new CleanWebpackPlugin(),
            new MiniCssExtractPlugin({
                filename: "[name].css"
            }),
            new webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery'
            })
        ]
    };
};